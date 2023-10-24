using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip;
using RymdRikedomar.Entities.SpaceShip.Modules;
using SpaceConsoleMenu;


public class TradingStation<T> : ITradingStation where T : IStoreItem
{
    public List<StoreItem<T>> AvailableItems { get; set; }
    private DisplayMenu TradingStationMenu { get; set; }


    public TradingStation(DisplayMenu _displayMenu, List<StoreItem<T>> availableItems)
    {
        TradingStationMenu = _displayMenu;
        AvailableItems = availableItems;
    }
    public void BuyGoods(Player player)
    {
        while (true)
        {
            var filteredItems = AvailableItems.Where(i => i.Item is IGood).ToList();

            // Generate dynamic menu options based on filtered items
            List<string> menuOptions = new();
            foreach (var item in filteredItems)
            {
                int maxCanBuy = player.Units / item.Item.PurchasePrice;
                string itemName = filteredItems.Find(i => i.Item.Name == item.Item.Name).Item.Name;
                int amount = player.Inventory.Find(i => i.Item.Name == item.Item.Name)?.Stock ?? 0;
                menuOptions.Add($"{item.Item.Name} (Pris: {item.Item.PurchasePrice} enheter, Tillgängligt: {item.Stock}, Du kan köpa: {Math.Min(maxCanBuy, item.Stock)} med dina nuvarande enheter)");
            }
            menuOptions.Add("Tillbaka");

            // Display prices and available units for filtered items
            foreach (var item in filteredItems)
            {
                Console.WriteLine($"{item.Item.Name} Pris: {item.Item.PurchasePrice}");
            }

            int buyChoice = TradingStationMenu.Menu($"Vilken vara vill du köpa?", menuOptions, $"Tillgängliga Enheter: {player.Units} enheter \n");

            if (buyChoice == menuOptions.Count - 1) // Last option is "Tillbaka"
            {
                break; // Return to the previous menu
            }
            else if (buyChoice >= 0 && buyChoice < filteredItems.Count)
            {
                Transaction(player, filteredItems[buyChoice].Item.Name, true);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            // Filter available items based on the provided product type
        }
    }





    public void SellGoods(Player player)
    {
        while (true)
        {
            // Filter available items based on the provided product type
            var filteredItems = AvailableItems.Where(i => i.Item is IGood).ToList();

            // Generate dynamic menu options based on filtered items
            List<string> menuOptions = new List<string>();
            foreach (var item in filteredItems)
            {
                int amount = player.Inventory.Find(i => i.Item.Name == item.Item.Name)?.Stock ?? 0;
                menuOptions.Add($"{item.Item.Name} (Pris: {item.Item.SellingPrice} enheter, Du har: {amount})");
            }
            menuOptions.Add("Tillbaka");

            // Display menu and get choice
            int choice = TradingStationMenu.Menu("Sell Goods", menuOptions, $"Tillgängliga Enheter: {player.Units} enheter \n");

            if (choice == menuOptions.Count - 1) // "Tillbaka" option
            {
                return;
            }
            else if (choice >= 0 && choice < filteredItems.Count)
            {
                Transaction(player, filteredItems[choice].Item.Name, false);
            }
            else
            {
                Console.WriteLine("Invalid choice.");

            }
        }
    }
    void Transaction(Player player, string goodName, bool isBuying)
    {
        Console.Clear();
        var selectedGoodEntry = AvailableItems.Find(item => item.Item.Name == goodName);


        if (selectedGoodEntry == null)
        {
            Console.WriteLine("Varan är inte tillgänglig!");
            Console.ReadKey();
            return;
        }

        T selectedGood = selectedGoodEntry.Item;
        int stock = selectedGoodEntry.Stock;
        int playerStock = player.Inventory.Find(i => i.Item.Name == goodName)?.Stock ?? 0;

        if (isBuying)
        {
            int maxBuyable = player.Units / selectedGood.PurchasePrice;
            string title = $"Köp {goodName}";
            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));
            Console.WriteLine($"Du har {player.Units} enheter tillgängliga.");
            Console.WriteLine($"Du har {playerStock} {goodName}.\n");

            Console.WriteLine($"{goodName}\n{selectedGood.PurchasePrice} enheter/st");
            Console.WriteLine($"Tillängligt: {stock} st\n");
            Console.WriteLine($"Med dina enheter kan du köpa {Math.Min(maxBuyable, stock)} {goodName}.");
            Console.WriteLine($"Hur många {goodName} vill du köpa?");
            Console.WriteLine("\n(Tryck på enter för att gå tillbaka)");
        }
        else
        {
            Console.WriteLine($"Du har {playerStock} {goodName}.");
            Console.WriteLine($"Du har {player.Units} enheter tillgängliga \n");

            Console.WriteLine($"Hur många {goodName} vill du sälja?");
            Console.WriteLine("(Tryck på enter för att gå tillbaka)");
        }

        if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
        {
            if (isBuying)
            {
                int totalCost = amount * selectedGood.PurchasePrice;
                if (totalCost <= player.Units && amount <= stock)
                {
                    selectedGoodEntry.BuyItem(amount, player);

                    if (typeof(T) == typeof(ISpaceshipModule))
                    {
                        Console.WriteLine($"Du köpte {goodName}.");
                    }
                    else
                    {
                        Console.WriteLine($"Du köpte {amount} {goodName} för {totalCost} enheter.");
                    }
                }
                else
                {
                    Console.WriteLine("Antingen har du inte råd, eller så finns det inte tillräckligt av varan.");
                }
            }
            else
            {
                int totalCost = amount * selectedGood.SellingPrice;

                if (amount <= playerStock)
                {
                    selectedGoodEntry.SellItem(amount, player);

                    Console.WriteLine($"Du sålde {amount} {goodName} för {totalCost} enheter.");
                }
                else
                {
                    Console.WriteLine($"Du har inte tillräckligt med {goodName} att sälja!");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        else
        {
            return;
        }
    }


    public void BuyFuel(Spaceship spaceship, Player player)
    {
        int fuelPrice = 1; // Assuming 1 unit of currency for 1 unit of fuel.
        float fuelNeeded = 100 - spaceship.Fuel; // Assuming 100 is the max fuel capacity.

        List<string> buyFuelOptions = new List<string>
            {
                $"Köp {fuelNeeded} enheter bränsle för {fuelNeeded * fuelPrice} enheter valuta (Fyll på)",
                $"Köp en anpassad mängd bränsle",
                "\nTillbaka till tanka menyn"
            };

        int choice = TradingStationMenu.Menu($"Tanka Rymdskeppet", buyFuelOptions, $"Tillgänglig bränsle: {spaceship.Fuel} enheter\nTillgängliga enheter valuta: {player.Units} enheter\n");

        switch (choice)
        {
            case 0: // Fill Up
                float cost = fuelNeeded * fuelPrice;
                if (cost <= player.Units)
                {
                    spaceship.Fuel += fuelNeeded;
                    player.Units -= (int)cost;
                    Console.WriteLine($"Successfully bought and added {fuelNeeded} units of fuel for {cost} units of currency.");
                }
                else
                {
                    Console.WriteLine($"You don't have enough currency to buy {fuelNeeded} units of fuel.");
                }
                break;

            case 1: // Buy custom amount
                Console.WriteLine($"How much fuel would you like to buy (0 to {fuelNeeded})?");

                if (float.TryParse(Console.ReadLine(), out float fuelToBuy))
                {
                    decimal customCost = (decimal)(fuelToBuy * fuelPrice);
                    if (fuelToBuy > 0 && fuelToBuy <= fuelNeeded && customCost <= player.Units)
                    {
                        spaceship.Fuel += fuelToBuy;
                        player.Units -= (int)customCost;
                        Console.WriteLine($"Successfully bought and added {fuelToBuy} units of fuel for {customCost} units of currency.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount or insufficient currency.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                break;

            case 2: // Return to refuel menu
                    // Do nothing and return
                break;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }


    public void ShowFuelStatus(Spaceship spaceship)
    {
        List<string> fuelStatusOptions = new List<string>
    {
        "Tillbaka till tanka menyn"
    };

        TradingStationMenu.Menu($"Bränslestatus ", fuelStatusOptions, $"Tillgänglig bränsle: {spaceship.Fuel} enheter");

        // Automatically returns to the refuel menu after displaying the status.
    }
}
