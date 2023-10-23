using System.Reflection.Metadata;
using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip;
using SpaceConsoleMenu;

public class TradingStation
{
    //Here we are using a Strategy Pattern to dependency inject certain goods to the tradingstation. 
    public List<(IGood Good, int Stock)> AvailableGoods { get; set; }
    private DisplayMenu TradingStationMenu { get; set; }

    public TradingStation(DisplayMenu _displayMenu)
    {
        TradingStationMenu = _displayMenu;
        AvailableGoods = new List<(IGood, int)>
        {
            (new Spice(), 100),
            (new Metal(), 100)
        };
    }

    public (int PurchasePrice, int SellingPrice) FindPricesByName(string name)
    {
        var (Good, Stock) = AvailableGoods.FirstOrDefault(g => g.Good.Name == name);

        if (Good != null)
        {
            return (Good.PurchasePrice, Good.SellingPrice);
        }

        return (0, 0);
    }

    public int FindStockByName(string name)
    {
        var (Good, Stock) = AvailableGoods.FirstOrDefault(g => g.Good.Name == name);

        if (Good != null)
        {
            return Stock;
        }

        return 0;
    }
    public (IGood Good, int Stock)? FindGoodByName(string name)
    {
        return AvailableGoods.FirstOrDefault(g => g.Good.Name == name);
    }

    public void UpdateStock(IGood good, int newStock)
    {
        var item = AvailableGoods.FirstOrDefault(g => g.Good == good);
        if (item.Good != null)
        {
            AvailableGoods.Remove(item);
            AvailableGoods.Add((item.Good, newStock));
        }
    }

    public void BuyGoods(Player player)
    {
        while (true)
        {
            Console.WriteLine(FindPricesByName("Krydda").PurchasePrice);
            Console.WriteLine(FindPricesByName("Metall").PurchasePrice);
            Console.WriteLine(player.Units);
            int maxKryddaCanBuy = player.Units / FindPricesByName("Krydda").PurchasePrice;
            int maxMetalCanBuy = player.Units / FindPricesByName("Metall").PurchasePrice;

            int choice = TradingStationMenu.Menu("Buy Goods", new List<string>
        {
            $"Krydda (Pris: {FindPricesByName("Krydda").PurchasePrice} enheter, Tillgängligt: {FindStockByName("Krydda")}, Du kan köpa: {Math.Min(maxKryddaCanBuy, FindStockByName("Krydda"))} med dina nuvarande enheter)",
            $"Metall (Pris: {FindPricesByName("Metall").PurchasePrice} units, Tillgänligt: {FindStockByName("Metall")}, Du kan köpa: {Math.Min(maxMetalCanBuy, FindStockByName("Metall"))} med dina nuvarande enheter)",
            "Tillbaka"
        }, "Tillgängliga enheter: " + player.Units + " units \n");

            switch (choice)
            {
                case 0: // Krydda
                    Transaction(player, "Krydda", true);
                    break;
                case 1: // Metall
                    Transaction(player, "Metall", true);
                    break;
                case 2: // Return
                    return;
            }
        }
    }




    public void SellGoods(Player player)
    {
        while (true)
        {
            int choice = TradingStationMenu.Menu("Sell Goods", new List<string>
        {
            $"Krydda (Pris: {FindPricesByName("Krydda").SellingPrice} enheter, Du har: {player.FindStockByName("Krydda")})",
            $"Metall (Pris: {FindPricesByName("Metall").SellingPrice} enheter, Du har {player.FindStockByName("Metall")})",
            "Tillbaka"
        }, "Tillgängliga Enheter: " + player.Units + " enheter \n");

            switch (choice)
            {
                case 0: // Krydda
                    Transaction(player, "Krydda", false);
                    break;
                case 1: // Metall
                    Transaction(player, "Metall", false);
                    break;
                case 2: // Return
                    return;
            }
        }
    }




    void Transaction(Player player, string goodName, bool isBuying)
    {
        Console.Clear();
        Console.WriteLine("Tillgängliga Enheter: " + player.Units + " enheter \n");
        var selectedGoodEntry = FindGoodByName(goodName);

        if (!selectedGoodEntry.HasValue)
        {
            Console.WriteLine("Varan är inte tillgänglig!");
            Console.ReadKey();
            return;
        }

        IGood selectedGood = selectedGoodEntry.Value.Good;
        int stock = selectedGoodEntry.Value.Stock;

        if (isBuying)
        {
            int maxBuyable = player.Units / selectedGood.PurchasePrice;

            Console.WriteLine($"Du har {player.Units} enheter tillgänlgiga.");
            Console.WriteLine($"{goodName}: {selectedGood.PurchasePrice} enheter/st.");
            Console.WriteLine($"Tillängligt: {stock}");
            Console.WriteLine($"Med dina enheter kan du köpa {Math.Min(maxBuyable, stock)} {goodName}.");
            Console.WriteLine($"Hur många {goodName} vill du köpa?");
        }
        else
        {
            Console.WriteLine($"Du har {player.FindStockByName(goodName)} {goodName}.");
            Console.WriteLine($"Hur många {goodName} vill du sälja?");
        }

        if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
        {


            if (isBuying)
            {
                int totalCost = amount * selectedGood.PurchasePrice;
                if (totalCost <= player.Units && amount <= stock)
                {
                    player.Units -= totalCost;
                    UpdateStock(selectedGood, stock - amount);
                    player.UpdateStock(selectedGood, player.FindStockByName(selectedGood.Name) + amount);

                    Console.WriteLine($"Du köpte {amount} {goodName} för {totalCost} enheter.");
                }
                else
                {
                    Console.WriteLine("Antingen har du inte råd, eller så finns det inte tillräckligt av varan.");
                }
            }
            else
            {
                int totalCost = amount * selectedGood.SellingPrice;

                if (amount <= player.FindStockByName(selectedGood.Name))
                {
                    player.Units += totalCost;
                    UpdateStock(selectedGood, stock + amount);
                    player.UpdateStock(selectedGood, player.FindStockByName(selectedGood.Name) - amount);

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
            Console.WriteLine("Skriv in ett giltigt nummer!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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