using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;

public class PirateEvent
{
    public delegate void PirateEventHandler(PirateEvent randomEvent);

    public event PirateEventHandler PirateEventEvent;

    StringPrinter stringPrinter = new StringPrinter();
    string pirateMsg1 = "Aaarrgghh.... Vi är rymdpirater!";
    string pirateMsg2 = "Du har två val...aaarghh... slåss eller ge oss material!";

    void PirateHandler(Player player)
    {
        stringPrinter.Print(pirateMsg1);
        stringPrinter.Print(pirateMsg2);


        int selectedIndex = 0;

        List<string> options = new List<string> { "Slåss", "Ge material" };
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

    static void SpacePirateAction(int selectedIndex, Player player)
    {
        Console.Clear();
        switch (selectedIndex)
        {
            case 0:
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

    static bool SpacePirateFight(Player player)
    {
        Random rnd = new Random();
        int random = rnd.Next(2 + player.Spaceship.WeaponDamage);
        if (random <= 0 + player.Spaceship.WeaponDamage)
        {
            Console.WriteLine("Du vann striden!");
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Tryck valfri tangent för att fortsätta");
            return true;
        }
        else
        {
            Console.WriteLine("Du förlorade striden och förlorade 1 hälsa!");
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine($"Du har {player.Spaceship.Health} hälsa kvar!");
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Tryck valfri tangent för att fortsätta");
            return false;
        }

    }

    static void SpacePirateDonation(Player player)
    {
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("Du har inget material! Förbered dig på att slåss!");
            Console.ReadKey();
            SpacePirateAction(0, player);
            return;
        }
        var good = player.Inventory
            .OrderByDescending(good => good.Stock)
            .Take(1)
            .Select(good => (good.Stock, good.Item));

        Console.WriteLine($"Piraterna tog {good.First().Stock / 2} {good.First().Item}!");
        Console.WriteLine(" ");
        Console.WriteLine("Tryck valfri tangent för att fortsätta...");



        var item = player.Inventory.Find(i => i.Item.Name == good.First().Item.Name);
        if (item != null)
        {
            item.Stock -= good.First().Stock / 2;
            return;
        }

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