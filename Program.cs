using System;
using System.Collections.Generic;
using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip.Modules;



namespace SpaceConsoleMenu
{
    public class TradingStation
    {
        public List<(IGood Good, int Stock)> AvailableGoods { get; set; }

        public TradingStation()
        {
            AvailableGoods = new List<(IGood, int)>
        {
            (new Spice(), 100),
            (new Metal(), 100)
        };
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
    }

    class Program
    {
        static void Main(string[] args)
        {

            TradingStation tradingStation = new TradingStation();
            Player player = new Player("Olle");
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                switch (DisplayMenu("Main Menu", new List<string>
                {
                    "Buy/Sell Goods",
                    "Upgrade Spaceship",
                    "Refuel",
                    "Travel to another planet",
                    "Exit"
                }))
                {
                    case 0:
                        switch (DisplayMenu("Buy/Sell Goods", new List<string>
                    {
                        "Buy Goods",
                        "Sell Goods",
                        "Return to Main Menu"
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
                        DisplayMenu("Upgrade Spaceship", new List<string>
                        {
                            "Upgrade Engine",
                            "Upgrade Weapons",
                            "Return to Main Menu"
                        }, "Available units: " + player.Units + " units \n");
                        break;

                    case 2:
                        DisplayMenu("Refuel", new List<string>
                        {
                            "Buy Fuel",
                            "Check Fuel Status",
                            "Return to Main Menu"
                        }, "Available units: " + player.Units + " units \n");
                        break;

                    case 3:
                        DisplayMenu("Travel to another planet", new List<string>
                        {
                            "Planet A",
                            "Planet B",
                            "Return to Main Menu"
                        });
                        break;

                    case 4:
                        exit = true;
                        break;
                }
            }
        }

        static void BuyGoods(TradingStation tradingStation, Player player)
        {
            while (true)
            {
                Console.WriteLine(tradingStation.FindGoodByName("Spice").Value.Good.PurchasePrice);
                Console.WriteLine(tradingStation.FindGoodByName("Metal").Value.Good.PurchasePrice);
                Console.WriteLine(player.Units);
                int maxSpiceCanBuy = player.Units / tradingStation.FindGoodByName("Spice").Value.Good.PurchasePrice;
                int maxMetalCanBuy = player.Units / tradingStation.FindGoodByName("Metal").Value.Good.PurchasePrice;

                int choice = DisplayMenu("Buy Goods", new List<string>
        {
            $"Spice (Price: {tradingStation.FindGoodByName("Spice").Value.Good.PurchasePrice} units, Available: {tradingStation.FindGoodByName("Spice").Value.Stock}, You can buy up to: {Math.Min(maxSpiceCanBuy, tradingStation.FindGoodByName("Spice").Value.Stock)} with your current units)",
            $"Metal (Price: {tradingStation.FindGoodByName("Metal").Value.Good.PurchasePrice} units, Available: {tradingStation.FindGoodByName("Metal").Value.Stock}, You can buy up to: {Math.Min(maxMetalCanBuy, tradingStation.FindGoodByName("Metal").Value.Stock)} with your current units)",
            "Return"
        }, "Available units: " + player.Units + " units \n");

                switch (choice)
                {
                    case 0: // Spice
                        Transaction(tradingStation, player, "Spice", true);
                        break;
                    case 1: // Metal
                        Transaction(tradingStation, player, "Metal", true);
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
            $"Spice (Price: {tradingStation.FindGoodByName("Spice").Value.Good.SellingPrice} units, You have: {player.FindGoodByName("Spice").Value.Stock})",
            $"Metal (Price: {tradingStation.FindGoodByName("Metal").Value.Good.SellingPrice} units, You have: {player.FindGoodByName("Metal").Value.Stock})",
            "Return"
        }, "Available units: " + player.Units + " units \n");

                switch (choice)
                {
                    case 0: // Spice
                        Transaction(tradingStation, player, "Spice", false);
                        break;
                    case 1: // Metal
                        Transaction(tradingStation, player, "Metal", false);
                        break;
                    case 2: // Return
                        return;
                }
            }
        }




        static void Transaction(TradingStation tradingStation, Player player, string goodName, bool isBuying)
        {
            Console.Clear();
            Console.WriteLine("Available units: " + player.Units + " units \n");
            var selectedGoodEntry = tradingStation.FindGoodByName(goodName);

            if (!selectedGoodEntry.HasValue)
            {
                Console.WriteLine("Selected good is not available!");
                Console.ReadKey();
                return;
            }

            IGood selectedGood = selectedGoodEntry.Value.Good;
            int stock = selectedGoodEntry.Value.Stock;

            if (isBuying)
            {
                int maxBuyable = player.Units / selectedGood.PurchasePrice;

                Console.WriteLine($"You currently have {player.Units} units.");
                Console.WriteLine($"Price of {goodName}: {selectedGood.PurchasePrice} units each.");
                Console.WriteLine($"Available in stock: {stock}");
                Console.WriteLine($"With your current units, you can buy up to {Math.Min(maxBuyable, stock)} {goodName}.");
                Console.WriteLine($"How many {goodName} would you like to buy?");
            }
            else
            {
                Console.WriteLine($"You have {player.FindGoodByName(goodName).Value.Stock} {goodName}.");
                Console.WriteLine($"How many {goodName} would you like to sell?");
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
                        player.UpdateStock(selectedGood, player.FindGoodByName(selectedGood.Name).Value.Stock + amount);

                        Console.WriteLine($"You bought {amount} {goodName} for {totalCost} units.");
                    }
                    else
                    {
                        Console.WriteLine("Either you can't afford this or there's not enough stock.");
                    }
                }
                else
                {
                    int totalCost = amount * selectedGood.SellingPrice;

                    if (amount <= player.FindGoodByName(selectedGood.Name).Value.Stock)
                    {
                        player.Units += totalCost;
                        tradingStation.UpdateStock(selectedGood, stock + amount);
                        player.UpdateStock(selectedGood, player.FindGoodByName(selectedGood.Name).Value.Stock - amount);

                        Console.WriteLine($"You sold {amount} {goodName} for {totalCost} units.");
                    }
                    else
                    {
                        Console.WriteLine($"You don't have enough {goodName} to sell!");
                    }
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
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
