using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;

public class PirateEvent
{
    public delegate void PirateEventHandler(PirateEvent randomEvent);

    public event PirateEventHandler PirateEventEvent;

    StringPrinter stringPrinter = new StringPrinter();
    string pirateMsg1 = "Aaarrgghh.... We are space pirates!";
    string pirateMsg2 = "You have two options...aaarghh... fight or gives us materials!";

    void PirateHandler(Player player)
    {
        stringPrinter.Print(pirateMsg1);
        stringPrinter.Print(pirateMsg2);


        int selectedIndex = 0;

        List<string> options = new List<string> { "Fight", "Give materials" };
        bool menuActive = true;

        while (menuActive)
        {
            Console.Clear();
            Console.WriteLine(pirateMsg1);
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine(pirateMsg2);
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");

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
                    menuActive = false;
                    SpacePirateAction(selectedIndex, player);
                    break;

            }
        }

    }

    void SpacePirateAction(int selectedIndex, Player player)
    {
        switch (selectedIndex)
        {
            case 0:
                Console.WriteLine("Are you sure you want to fight?");
                Console.ReadKey();
                bool fightCondition = SpacePirateFight(player);
                if (fightCondition)
                {
                    player.DefeatedPirates++;
                }
                else
                {
                    player.Spaceship.Health--;
                }
                break;
            case 1:
                SpacePirateDonation(player);
                break;
        }
    }

    bool SpacePirateFight(Player player)
    {
        Random rnd = new Random();
        int random = rnd.Next(2 + player.Spaceship.WeaponDamage);
        if (random <= 0 + player.Spaceship.WeaponDamage)
        {
            Console.WriteLine("You won the fight!");
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Press any key to continue...");
            return true;
        }
        else
        {
            Console.WriteLine("You lost the fight and lost 1 health!");
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Press any key to continue...");
            return false;
        }

    }

    static void SpacePirateDonation(Player player)
    {
        var good = player.Inventory
            .OrderByDescending(good => good.Stock)
            .Take(1)
            .Select(good => (good.Stock, good.Good));

        if (good != null)
        {
            RemoveFromInventory(good.First().Good, good.First().Stock / 2, player);
        }



        Console.WriteLine($"The pirates took {good.First().Stock / 2} {good.First().Good}!");
        Console.WriteLine(good.First().Good + " " + good.First().Stock);
        Console.WriteLine(" ");
        Console.WriteLine("Press any key to continue...");


    }

    static void RemoveFromInventory(IGood good, int amount, Player player)
    {
        var inventoryItem = player.Inventory.FirstOrDefault(item => item.Good == good);

        if (inventoryItem.Stock > 0)
        {
            inventoryItem.Stock -= amount;
            Console.WriteLine(inventoryItem.Stock);

            if (inventoryItem.Stock < 0)
            {
                player.Inventory.Remove(inventoryItem);
            }
        }
        Console.WriteLine(inventoryItem.Stock);
    }

    public void OnRandomEvent(PirateEvent pirateEvent, Player player)
    {
        if (PirateEventEvent != null)
        {
            PirateHandler(player);
            PirateEventEvent(pirateEvent);
        }
    }
}