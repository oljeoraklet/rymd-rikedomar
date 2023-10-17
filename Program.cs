using System;
using System.Collections.Generic;

namespace SpaceConsoleMenu
{
    public class TradingStation
    {
        public List<(BaseGood Good, int Stock)> AvailableGoods { get; set; }

        public TradingStation()
        {
            AvailableGoods = new List<(BaseGood, int)>
        {
            (new Spice(), 100),
            (new Metal(), 100)
        };
        }

        public (BaseGood Good, int Stock)? FindGoodByName(string name)
        {
            return AvailableGoods.FirstOrDefault(g => g.Good.Name == name);
        }

        public void UpdateStock(BaseGood good, int newStock)
        {
            var item = AvailableGoods.FirstOrDefault(g => g.Good == good);
            if (item.Good != null)
            {
                AvailableGoods.Remove(item);
                AvailableGoods.Add((item.Good, newStock));
            }
        }
    }


    public class Player
    {
        public int Units { get; set; } = 1000;
        public Dictionary<string, int> Goods { get; set; } = new Dictionary<string, int>
    {
        { "Spice", 0 },
        { "Metal", 0 }
    };
    }




    class Program
    {
        static void Main(string[] args)
        {

            TradingStation tradingStation = new TradingStation();
            Player player = new Player();
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
                        });
                        break;

                    case 2:
                        DisplayMenu("Refuel", new List<string>
                        {
                            "Buy Fuel",
                            "Check Fuel Status",
                            "Return to Main Menu"
                        });
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
                int maxSpiceCanBuy = player.Units / tradingStation.FindGoodByName("Spice").Value.Good.PurchasePrice;
                int maxMetalCanBuy = player.Units / tradingStation.FindGoodByName("Metal").Value.Good.PurchasePrice;

                int choice = DisplayMenu("Buy Goods", new List<string>
        {
            $"Spice (Price: {tradingStation.FindGoodByName("Spice").Value.Good.PurchasePrice} units, Available: {tradingStation.FindGoodByName("Spice").Value.Stock}, You can buy up to: {Math.Min(maxSpiceCanBuy, tradingStation.FindGoodByName("Spice").Value.Stock)} with your current units)",
            $"Metal (Price: {tradingStation.FindGoodByName("Metal").Value.Good.PurchasePrice} units, Available: {tradingStation.FindGoodByName("Metal").Value.Stock}, You can buy up to: {Math.Min(maxMetalCanBuy, tradingStation.FindGoodByName("Metal").Value.Stock)} with your current units)",
            "Return"
        });

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
            $"Spice (Price: {tradingStation.FindGoodByName("Spice").Value.Good.SellingPrice} units, You have: {player.Goods["Spice"]})",
            $"Metal (Price: {tradingStation.FindGoodByName("Metal").Value.Good.SellingPrice} units, You have: {player.Goods["Metal"]})",
            "Return"
        });

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
            var selectedGoodEntry = tradingStation.FindGoodByName(goodName);

            if (!selectedGoodEntry.HasValue)
            {
                Console.WriteLine("Selected good is not available!");
                Console.ReadKey();
                return;
            }

            BaseGood selectedGood = selectedGoodEntry.Value.Good;
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
                Console.WriteLine($"You have {player.Goods[goodName]} {goodName}.");
                Console.WriteLine($"How many {goodName} would you like to sell?");
            }

            if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
            {
                int totalCost = amount * selectedGood.SellingPrice;

                if (isBuying)
                {
                    if (totalCost <= player.Units && amount <= stock)
                    {
                        player.Units -= totalCost;
                        tradingStation.UpdateStock(selectedGood, stock - amount);
                        player.Goods[goodName] += amount;

                        Console.WriteLine($"You bought {amount} {goodName} for {totalCost} units.");
                    }
                    else
                    {
                        Console.WriteLine("Either you can't afford this or there's not enough stock.");
                    }
                }
                else
                {
                    if (amount <= player.Goods[goodName])
                    {
                        player.Units += totalCost;
                        tradingStation.UpdateStock(selectedGood, stock + amount);
                        player.Goods[goodName] -= amount;

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




        static int DisplayMenu(string title, List<string> options)
        {
            int selectedIndex = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine(new string('-', title.Length));
                Console.WriteLine();

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
