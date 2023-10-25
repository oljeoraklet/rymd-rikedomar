using System;
using System.Linq;
using System.Collections.Generic;
using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip.Modules;
using RymdRikedomar.Entities.SpaceShip;
using System.Net.NetworkInformation;
using RymdRikedomar.Services.EndGameConditions;

namespace SpaceConsoleMenu
{
    class Program
    {
        static bool turnOver;
        static int turnCounter = 0;
        static void Main(string[] args)
        {

            TradingStationFactory tradingStationFactory = new();

            //Här använder i ett "Strategy Pattern"
            //Vi använder detta genom att skapa en TradingStation med som har olika varor beroende på vilken planet vi är på, som sedan dependency injectas in i planeten.
            //Vi använder Strategy Pattern för att kunna skapa olika beteenden på en planet genom att ge planeterna olika utbud.

            Planet Zephyria = new("Zephyria", 0, tradingStationFactory.createTradingStation());
            Planet Bobo = new("Bobo", 2, tradingStationFactory.createTradingStation());
            Planet Aquillon = new("Aquillon", 5, tradingStationFactory.createTradingStation());
            Planet Pyralis = new("Pyralis", 6, tradingStationFactory.createTradingStation());
            Planet Astronia = new("Astronia", 8, tradingStationFactory.createTradingStation());
            Planet Terravox = new("Terravox", 11, tradingStationFactory.createTradingStation());
            Planet Luminara = new("Luminara", 12, tradingStationFactory.createTradingStation());
            Planet Dracoria = new("Dracoria", 15, tradingStationFactory.createTradingStation());
            Planet Nebulon = new("Nebulon", 17, tradingStationFactory.createTradingStation());
            Planet Celestria = new("Celestria", 18, tradingStationFactory.createTradingStation());
            Planet Volteron = new("Volteron", 20, tradingStationFactory.createTradingStation());


            //Här använder vi Collections
            //Vi använder en Colllection genom att initializera en lista där typ-parametern är en Planet. Vi lägger sedan till Planeter till vår lista.
            //Detta gör vi för att vi vill kunna spara och hålla alla planeter vi skapar och för att vi ska kunna få tillgång till dem i vårt spel.

            //I och med denna collection använder vi också Generics
            //Vi använder en "List<T>" och använder denna generiska lista för att spara Planets
            //Vi vill göra detta för att ha någonstans att spara våra planeter. 
            List<Planet> planets = new();

            planets.Add(Zephyria);
            planets.Add(Bobo);
            planets.Add(Aquillon);
            planets.Add(Pyralis);
            planets.Add(Astronia);
            planets.Add(Terravox);
            planets.Add(Luminara);
            planets.Add(Dracoria);
            planets.Add(Nebulon);
            planets.Add(Celestria);
            planets.Add(Volteron);

            //Här använder vi Collection Initializers
            //Vi skapar en lista av endgameconditions och istället för att använda ordet "add" så använder vi oss av en Collection Initializer för att lägga till våra endgameconditions.
            //Vi använder detta för att på ett snyggt och smidigt sätt skapa en lista av endgameconditions, genom att minska rader kod och skapa bättre readability.

            List<IEndGameCondition> EndGameConditions = new() { new SpaceCop(), new Diplomat(), new Explorer(), new Capitalist() };


            void Print(string str)
            {
                foreach (char c in str)
                {
                    Console.Write(c);
                    Thread.Sleep(40);
                }
                Console.WriteLine(" ");

            }


            // Console.Clear();
            // Print("Välkommen till RymdRikedomar!");
            // Console.WriteLine(" ");
            // Print("Spelet går ut på att du ska utforska ett universum, där du kan besöka olika planeter och handla med dem.");
            // Console.WriteLine(" ");
            // Print("Du kan vinna på fyra olika sätt:");
            // Console.WriteLine(" ");
            // Console.WriteLine("1. Bli en rymdpolis genom att besegra fem rymdpirater!");
            // Console.WriteLine("2. Utforska alla planeter i universumet");
            // Console.WriteLine("3. Bli inflytelserik genom att skapa goda relationer genom donationer till planeterna.");
            // Console.WriteLine("4. Vinn genom att bli så rik som möjligt!");
            // Console.WriteLine(" ");
            // Print("Tryck valfri tangent för att fortsätta...");
            // Console.ReadKey();
            // Console.Clear();
            // Print("Vad heter du?");
            // string playerName = Console.ReadLine();
            // Console.Clear();
            // Print("Tack!");
            // Console.WriteLine(" ");
            // Console.WriteLine("Tryck på valfri tangent för att sätta igång spelet.");
            // Console.ReadKey();

            // if (playerName == null)
            // {
            //     playerName = "Utforskare";
            // }

            Player player = new("adg", EndGameConditions);
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

            //Här använder vi multicast delegater
            // Vi vill använda multicast delegater för att kunna skapa en prenumeration för spelaren till eventsen.
            // Vi använder multicast delegater för att kunna prenumerera på flera events samtidigt.
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

            while (!exit && !player.hasWon)
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
                        Console.Clear();
                        Print($"Du landar på {currentPlanet.Name}...");
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
                                    currentPlanet.TradingStation.BuyGoods(player);
                                    break;
                                case 1: // Sell Goods
                                    currentPlanet.TradingStation.SellGoods(player);
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
                player.notifyConditions();

                if (player.hasWon)
                {
                    Console.Clear();
                    string endGameText = $"Grattis! Du har vunnit spelet som {player.winningCondition.ConditionName}";
                    Console.WriteLine(endGameText);
                    Console.WriteLine(" ");
                    Console.WriteLine(new string('-', endGameText.Length));
                    Console.WriteLine(" ");
                    Console.WriteLine("Tryck valfri tangent för att avsluta spelet...");

                    Console.ReadKey();
                    exit = true;
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

            //Här används LINQ
            //Vi använder LINQ för att kunna sortera planeterna efter avstånd från nuvarande planet.
            //Vi använder LINQ för att kunna ta de tre närmsta planeterna från nuvarande planet.

            //Vi använder här även Lambdas i funktionerna
            //Vi använder Lambdas för att kunna skapa en funktion som tar in ett argument och returnerar ett värde.
            //Vi använder Lambdas för att kunna dra nytta av LINQ på ett enkelt och effektivt sätt, än att skapa massa funktioner som vi bara använder en gång.
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



