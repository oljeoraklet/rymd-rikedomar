using System;
using System.Linq;
using System.Collections.Generic;
using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip.Modules;
using RymdRikedomar.Entities.SpaceShip;



namespace SpaceConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Planet> planets = new() { new("Zephyria", 0), new("Bobo", 2), new("Aquillon", 5), new("Pyralis", 6), new("Astronia", 8), new("Terravox", 11), new("Luminara", 12), new("Dracoria", 15), new("Nebulon", 17), new("Celestria", 18), new("Volteron", 20) };

            Planet currentPlanet = planets.First(p => p.Name == "Zephyria");

            Spaceship spaceship = new();

            TradingStation tradingStation = new();
            Player player = new("Olle");
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                switch (DisplayMenu($"Välkommen till {currentPlanet.Name}", new List<string>
                {
                    "Köp/Sälj Varor",
                    "Uppgradera Rymdskeppet",
                    "Tanka Rymdskeppet",
                    "Res till en annan planet",
                    "Avsluta"
                }))
                {
                    case 0:
                        switch (DisplayMenu($"Köp/Sälj Varor - {currentPlanet.Name}", new List<string>
                    {
                        "Köp Varor",
                        "Sälj Varor",
                        "Tillbaka Till Huvudmenyn"
                    }))
                        {
                            case 0: // Buy Goods
                                BuyGoods(tradingStation, player);
                                break;

                            case 1: // Sell Goods
                                SellGoods(tradingStation, player);
                                break;
                        }
                        break;

                    case 1:
                        DisplayMenu($"Uppgradera Rymdskeppet - {currentPlanet.Name}", new List<string>
                        {
                            "Uppgradera Motor",
                            "Uppgradera Vapen",
                            "Tillbaka till Huvudmenyn"
                        }, "Tillgängliga Enheter: " + player.Units + " enheter \n");
                        break;

                    case 2:
                        int refuelChoice = DisplayMenu($"Tanka Rymdskeppet  - {currentPlanet.Name}", new List<string>
                        {
                            "Köp Bränsle",
                            "Bränslestatus",
                            "Tillbaka till Huvudmenyn"
                        }, "Tillgängliga Enheter: " + player.Units + " enheter \n");

                        switch (refuelChoice)
                        {
                            case 0: // Buy Fuel
                                BuyFuel(spaceship, player);
                                break;
                            case 1: // Fuel Status
                                ShowFuelStatus(spaceship);
                                break;
                            case 2: // Return to Main Menu
                                    // Do nothing, just return to the main menu.
                                break;
                        }
                        break;


                    case 3: // Travel to another planet
                        currentPlanet = TravelToAnotherPlanet(planets, currentPlanet, spaceship);
                        break;



                    case 4:
                        exit = true;
                        break;
                }
            }
        }
        public static List<Planet> FindThreeClosestPlanets(List<Planet> allPlanets, Planet currentPlanet)
        {
            return allPlanets.Where(p => p != currentPlanet)
                             .OrderBy(p => Math.Abs(p.xDistance - currentPlanet.xDistance))
                             .Take(3)
                             .ToList();
        }

        public static Planet TravelToAnotherPlanet(List<Planet> planets, Planet currentPlanet, Spaceship spaceship)
        {
            var sortedPlanets = planets.OrderBy(p => Math.Abs(p.xDistance - currentPlanet.xDistance))
                                        .Where(p => p != currentPlanet)
                                        .Take(3)
                                        .ToList();

            List<string> planetMenuOptions = sortedPlanets.Select(p =>
            {
                int distance = Math.Abs(p.xDistance - currentPlanet.xDistance);
                return $"{p.Name} (Avstånd: {distance} parsecs, Bränslekostnad: {distance})";
            }).ToList();

            planetMenuOptions.Add("Tillbaka till Huvudmenyn");

            int choice = DisplayMenu($"Res till en annan planet - {currentPlanet.Name}", planetMenuOptions);

            if (choice == planetMenuOptions.Count - 1)
            {
                return currentPlanet; // Return without changing planet if "Return to Main Menu" is selected
            }
            else
            {
                int distance = Math.Abs(sortedPlanets[choice].xDistance - currentPlanet.xDistance);
                float fuelNeeded = distance / spaceship.FuelEfficiency;

                if (spaceship.Fuel >= fuelNeeded)
                {
                    spaceship.ConsumeFuel(distance);
                    return sortedPlanets[choice];
                }
                else
                {
                    Console.WriteLine("Inte tillräckligt med bränsle för denna resa!");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    return currentPlanet; // If not enough fuel, remain on the current planet
                }
            }
        }

        public static void BuyFuel(Spaceship spaceship, Player player)
        {
            int fuelPrice = 1; // Assuming 1 unit of currency for 1 unit of fuel.
            float fuelNeeded = 100 - spaceship.Fuel; // Assuming 100 is the max fuel capacity.

            List<string> buyFuelOptions = new List<string>
    {
        $"Köp {fuelNeeded} enheter bränsle för {fuelNeeded * fuelPrice} enheter valuta (Fyll på)",
        $"Köp en anpassad mängd bränsle",
        "\nTillbaka till tanka menyn"
    };

            int choice = DisplayMenu($"Tanka Rymdskeppet", buyFuelOptions, $"Tillgänglig bränsle: {spaceship.Fuel} enheter\nTillgängliga enheter valuta: {player.Units} enheter\n");

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


        public static void ShowFuelStatus(Spaceship spaceship)
        {
            List<string> fuelStatusOptions = new List<string>
    {
        "Tillbaka till tanka menyn"
    };

            DisplayMenu($"Bränslestatus ", fuelStatusOptions, $"Tillgänglig bränsle: {spaceship.Fuel} enheter");

            // Automatically returns to the refuel menu after displaying the status.
        }


        static void BuyGoods(TradingStation tradingStation, Player player)
        {
            while (true)
            {
                Console.WriteLine(tradingStation.FindPricesByName("Krydda").PurchasePrice);
                Console.WriteLine(tradingStation.FindPricesByName("Metall").PurchasePrice);
                Console.WriteLine(player.Units);
                int maxKryddaCanBuy = player.Units / tradingStation.FindPricesByName("Krydda").PurchasePrice;
                int maxMetalCanBuy = player.Units / tradingStation.FindPricesByName("Metall").PurchasePrice;

                int choice = DisplayMenu("Buy Goods", new List<string>
        {
            $"Krydda (Pris: {tradingStation.FindPricesByName("Krydda").PurchasePrice} enheter, Tillgängligt: {tradingStation.FindStockByName("Krydda")}, Du kan köpa: {Math.Min(maxKryddaCanBuy, tradingStation.FindStockByName("Krydda"))} med dina nuvarande enheter)",
            $"Metall (Pris: {tradingStation.FindPricesByName("Metall").PurchasePrice} units, Tillgänligt: {tradingStation.FindStockByName("Metall")}, Du kan köpa: {Math.Min(maxMetalCanBuy, tradingStation.FindStockByName("Metall"))} med dina nuvarande enheter)",
            "Tillbaka"
        }, "Tillgängliga enheter: " + player.Units + " units \n");

                switch (choice)
                {
                    case 0: // Krydda
                        Transaction(tradingStation, player, "Krydda", true);
                        break;
                    case 1: // Metall
                        Transaction(tradingStation, player, "Metall", true);
                        break;
                    case 2: // Return
                        return;
                }
            }
        }




        static void SellGoods(TradingStation tradingStation, Player player)
        {
            while (true)
            {
                int choice = DisplayMenu("Sell Goods", new List<string>
        {
            $"Krydda (Price: {tradingStation.FindPricesByName("Krydda").SellingPrice} enheter, Du har: {player.FindStockByName("Krydda")})",
            $"Metall (Price: {tradingStation.FindPricesByName("Metall").SellingPrice} enheter, Du har {player.FindStockByName("Metall")})",
            "Tillbaka"
        }, "Tillgängliga Enheter: " + player.Units + " enheter \n");

                switch (choice)
                {
                    case 0: // Krydda
                        Transaction(tradingStation, player, "Krydda", false);
                        break;
                    case 1: // Metall
                        Transaction(tradingStation, player, "Metall", false);
                        break;
                    case 2: // Return
                        return;
                }
            }
        }




        static void Transaction(TradingStation tradingStation, Player player, string goodName, bool isBuying)
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga Enheter: " + player.Units + " enheter \n");
            var selectedGoodEntry = tradingStation.FindGoodByName(goodName);

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
                        tradingStation.UpdateStock(selectedGood, stock - amount);
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
                        tradingStation.UpdateStock(selectedGood, stock + amount);
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




        static int DisplayMenu(string title, List<string> options, string currency = "")
        {
            int selectedIndex = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine(new string('-', title.Length));
                Console.WriteLine(currency);

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine(options[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(options[i]);
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0) selectedIndex--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedIndex < options.Count - 1) selectedIndex++;
                        break;

                    case ConsoleKey.Enter:
                        return selectedIndex;
                }
            }
        }
    }


}


