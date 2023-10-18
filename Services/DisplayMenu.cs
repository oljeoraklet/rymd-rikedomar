using System;
using System.Collections.Generic;

namespace SpaceConsoleMenu
{
    class DisplayMenu
    {
        public static int Menu(string title, List<string> options, string currency = "")
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