using System;
using System.Linq;
using System.Collections.Generic;
using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip.Modules;
using RymdRikedomar.Entities.SpaceShip;
using System.Net.NetworkInformation;

namespace SpaceConsoleMenu
{
    class Program
    {
        static bool turnOver;
        static int turnCounter = 0;
        static void Main(string[] args)
        {
            Player player = new("Olle");
            List<Planet> planets = new() { new("Zephyria", 0), new("Bobo", 2), new("Aquillon", 5), new("Pyralis", 6), new("Astronia", 8), new("Terravox", 11), new("Luminara", 12), new("Dracoria", 15), new("Nebulon", 17), new("Celestria", 18), new("Volteron", 20) };
            Planet currentPlanet = planets.First(p => p.Name == "Zephyria");
            player.VisitedPlanets.Add(currentPlanet);
            Spaceship spaceship = new();
            DisplayMenu menu = new();
            bool exit = false;


            //Init events
            MarketBoom marketBoom = new MarketBoom();
            PirateEvent pirateEvent = new PirateEvent();
            NoEvent noEvent = new NoEvent();
            DonationEvent donationEvent = new DonationEvent();

            //Subscribe to events using multicast delegate
            marketBoom.MarketBoomEvent += player.MarketBoomEventHandler;
            pirateEvent.PirateEventEvent += player.PirateEventHandler;
            noEvent.NoEventEvent += player.NoEventHandler;
            donationEvent.DonationEventEvent += player.DonationEventHandler;


            void RandomEvent()
            {
                Random rnd = new Random();
                int random = rnd.Next(4);
                switch (random)
                {
                    case 0:
                        marketBoom.OnRandomEvent(marketBoom, planets);
                        break;
                    case 1:
                        pirateEvent.OnRandomEvent(pirateEvent, player);
                        break;
                    case 2:
                        noEvent.OnRandomEvent(noEvent);
                        break;
                    case 3:
                        donationEvent.OnRandomEvent(donationEvent, currentPlanet, player);
                        break;
                    default:
                        break;
                }
            }

            while (!exit)
            {
                turnOver = false;
                bool eventOver = false;

                if (turnCounter != 0)
                {
                    while (!eventOver)
                    {
                        Console.Clear();
                        RandomEvent();
                        Console.ReadKey();
                        eventOver = true;
                    }

                }


                while (!turnOver)
                {
                    Console.Clear();
                    switch (menu.Menu($"Välkommen till {currentPlanet.Name}", new List<string>
                {
                    "Köp/Sälj Varor",
                    "Uppgradera Rymdskeppet",
                    "Tanka Rymdskeppet",
                    "Res till en annan planet",
                    "Avsluta",
                }))
                    {
                        case 0:
                            switch (menu.Menu($"Köp/Sälj Varor - {currentPlanet.Name}", new List<string>
                    {
                        "Köp Varor",
                        "Sälj Varor",
                        "Tillbaka Till Huvudmenyn"
                    }))
                            {
                                case 0: // Buy Goods
                                    int buyChoice = menu.Menu($"Vilken vara vill du köpa?", new List<string>
                                    {
                                        "Krydda",
                                        "Metall",
                                        "Tillbaka"
                                    });

                                    switch (buyChoice)
                                    {
                                        case 0: // Krydda
                                            currentPlanet.TradingStation.BuyGoods(player, "Krydda");
                                            break;
                                        case 1: // Metall
                                            currentPlanet.TradingStation.BuyGoods(player, "Metall");
                                            break;
                                        case 2: // Return to previous menu
                                            break;
                                    }
                                    break;

                                case 1: // Sell Goods
                                    int sellChoice = menu.Menu($"Vilken vara vill du sälja?", new List<string>
                                    {
                                        "Krydda",
                                        "Metall",
                                        "Tillbaka"
                                    });

                                    switch (sellChoice)
                                    {
                                        case 0: // Krydda
                                            currentPlanet.TradingStation.SellGoods(player, "Krydda");
                                            break;
                                        case 1: // Metall
                                            currentPlanet.TradingStation.SellGoods(player, "Metall");
                                            break;
                                        case 2: // Return to previous menu
                                            break;
                                    }
                                    break;
                            }
                            break;

                        case 1:
                            menu.Menu($"Uppgradera Rymdskeppet - {currentPlanet.Name}", new List<string>
                        {
                            "Uppgradera Motor",
                            "Uppgradera Vapen",
                            "Tillbaka till Huvudmenyn"
                        }, "Tillgängliga Enheter: " + player.Units + " enheter \n");
                            break;
                        case 2:
                            int refuelChoice = menu.Menu($"Tanka Rymdskeppet  - {currentPlanet.Name}", new List<string>
                        {
                            "Köp Bränsle",
                            "Bränslestatus",
                            "Tillbaka till Huvudmenyn"
                        }, "Tillgängliga Enheter: " + player.Units + " enheter \n");

                            switch (refuelChoice)
                            {
                                case 0: // Buy Fuel
                                    currentPlanet.TradingStation.BuyFuel(spaceship, player);
                                    break;
                                case 1: // Fuel Status
                                    currentPlanet.TradingStation.ShowFuelStatus(spaceship);
                                    break;
                                case 2: // Return to Main Menu
                                        // Do nothing, just return to the main menu.
                                    break;
                            }
                            break;


                        case 3: // Travel to another planet
                            currentPlanet = TravelToAnotherPlanet(planets, currentPlanet, spaceship, player, menu);
                            break;

                        case 4:
                            exit = true;
                            turnOver = true;
                            break;
                    }


                }
            }
        }
        public static List<Planet> FindThreeClosestPlanets(List<Planet> allPlanets, Planet currentPlanet)
        {
            return allPlanets.Where(p => p != currentPlanet)
                             .OrderBy(p => Math.Abs(p.XDistance - currentPlanet.XDistance))
                             .Take(3)
                             .ToList();
        }



        public static Planet TravelToAnotherPlanet(List<Planet> planets, Planet currentPlanet, Spaceship spaceship, Player player, DisplayMenu menu)
        {
            var sortedPlanets = planets.OrderBy(p => Math.Abs(p.XDistance - currentPlanet.XDistance))
                                        .Where(p => p != currentPlanet)
                                        .Take(3)
                                        .ToList();

            List<string> planetMenuOptions = sortedPlanets.Select(p =>
            {
                int distance = Math.Abs(p.XDistance - currentPlanet.XDistance);
                return $"{p.Name} (Avstånd: {distance} parsecs, Bränslekostnad: {distance})";
            }).ToList();

            planetMenuOptions.Add("Tillbaka till Huvudmenyn");

            int choice = menu.Menu($"Res till en annan planet - {currentPlanet.Name}", planetMenuOptions);

            if (choice == planetMenuOptions.Count - 1)
            {
                return currentPlanet; // Return without changing planet if "Return to Main Menu" is selected
            }
            else
            {
                int distance = Math.Abs(sortedPlanets[choice].XDistance - currentPlanet.XDistance);
                float fuelNeeded = distance / spaceship.FuelEfficiency;

                if (spaceship.Fuel >= fuelNeeded)
                {
                    spaceship.ConsumeFuel(distance);
                    player.VisitedPlanets.Add(sortedPlanets[choice]);
                    turnCounter++;
                    turnOver = true;
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
    }
}



