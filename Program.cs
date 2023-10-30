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

        static TradingStationFactory tradingStationFactory = new();
        static IEnumerable<Planet> planets = GeneratePlanets(tradingStationFactory);
        static IEnumerator<Planet> planetEnumerator = GeneratePlanets(tradingStationFactory).GetEnumerator();
        static Planet? currentPlanet;
        static List<Planet> discoveredPlanets = new();

        static List<Planet>? closestPlanets;
        static int turnCounter = 0;
        static void Main(string[] args)
        {

            //1. Här använder vi Collections
            //2. Vi använder en Colllection genom att initializera en lista där typ-parametern är en IEndGameCondition. Vi lägger sedan till alla olika nuvarande EndGameConditions i listan.
            //3. Vi vill använda en collection med alla typer av EndGameConditions för att kunna iterera över dessa och kalla på deras funktioner för att se om spelaren har vunnit eller inte.

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


            Console.Clear();
            Print("Välkommen till RymdRikedomar!");
            Console.WriteLine(" ");
            Print("Spelet går ut på att du ska utforska ett universum, där du kan besöka olika planeter och handla med dem.");
            Console.WriteLine(" ");
            Print("Du kan vinna på fyra olika sätt:");
            Console.WriteLine(" ");
            Console.WriteLine("1. Bli en rymdpolis genom att besegra fem rymdpirater!");
            Console.WriteLine("2. Utforska alla planeter i universumet");
            Console.WriteLine("3. Bli inflytelserik genom att skapa goda relationer genom donationer till planeterna.");
            Console.WriteLine("4. Vinn genom att bli så rik som möjligt!");
            Console.WriteLine(" ");
            Print("Tryck valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
            Print("Vad heter du?");
            string playerName = Console.ReadLine();
            Console.Clear();
            Print("Tack!");
            Console.WriteLine(" ");
            Console.WriteLine("Tryck på valfri tangent för att sätta igång spelet.");
            Console.ReadKey();

            if (playerName == null)
            {
                playerName = "Utforskare";
            }

            Player player = new(playerName, EndGameConditions);


            currentPlanet = new Planet("Tellus", tradingStationFactory.createTradingStation(), 0);
            player.VisitedPlanets.Add(currentPlanet);
            closestPlanets = planets.Take(3).ToList();
            DisplayMenu menu = new();
            bool exit = false;


            //Init events
            MarketBoom marketBoom = new MarketBoom();
            PirateEvent pirateEvent = new PirateEvent();
            NoEvent noEvent = new NoEvent();
            DonationEvent donationEvent = new DonationEvent();

            // 1.Här använder vi multicast delegater
            // 2. Vi använder multicast delegaterna och spelaren genom att "addera" spelaren till eventen. Detta gör att vi kan prenumerera på eventsen och få utskick när de inträffar.
            // 3. Multicast delegater möjliggör i ett senare skede att få fler spelare att prenumera på eventsen. Om ett flerspelarläge eller liknande skulle implementeras, kan man enkelt 
            // använda samma sätt för att få de andra spelarna att prenumerera på eventsen.


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
                        donationEvent.OnRandomEvent(donationEvent, currentPlanet, player);
                        break;
                    case 1:
                        pirateEvent.OnRandomEvent(pirateEvent, player);
                        break;
                    case 2:
                        noEvent.OnRandomEvent(noEvent);
                        break;
                    case 3:
                        marketBoom.OnRandomEvent(marketBoom, planets.ToList());
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
                    "Profil",
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
                                    currentPlanet.TradingStation.BuyFuel(player);
                                    break;
                                case 1: // Fuel Status
                                    currentPlanet.TradingStation.ShowFuelStatus(player);
                                    break;
                                case 2: // Return to Main Menu
                                        // Do nothing, just return to the main menu.
                                    break;
                            }
                            break;


                        case 3: // Travel to another planet
                            currentPlanet = TravelToAnotherPlanet(currentPlanet, player, menu);
                            break;

                        case 4: // Profile
                            int profileChoice = menu.Menu($"Profil - {currentPlanet.Name}", new List<string>
                        {
                            "Visa förråd",
                            "Besökta planeter",
                            "Upptäckta planeter",
                            "Tillbaka till Huvudmenyn"
                        }, "Tillgängliga Enheter: " + player.Units + " enheter \n");
                            switch (profileChoice)
                            {
                                case 0:
                                    Console.WriteLine(" ");
                                    Console.WriteLine("--- FÖRRÅD ---");
                                    Console.WriteLine(" ");
                                    ProfileInventory profileInventory = new(player.Inventory);
                                    profileInventory.ShowItems();
                                    Console.ReadKey();
                                    break;
                                case 1:
                                    Console.WriteLine(" ");
                                    Console.WriteLine("--- BESÖKTA PLANETER ---");
                                    Console.WriteLine(" ");
                                    ProfileVisitedPlanets visitedPlanets = new(player.VisitedPlanets);
                                    visitedPlanets.ShowItems();
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.WriteLine(" ");
                                    Console.WriteLine("--- UPPTÄCKTA PLANETER ---");
                                    Console.WriteLine(" ");
                                    ProfileDiscoveredPlanets profile = new ProfileDiscoveredPlanets(discoveredPlanets);
                                    profile.ShowItems();
                                    Console.ReadKey();
                                    break;
                            }
                            break;

                        case 5:
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


        //1, Här skapar vi en IEnumerable i returtypen av funktionen.
        //2. Funktionen GeneratePlanets skapar en lista av namn på planeter, som sedan slumpmässigt väljs ut och skapar en planet med ett namn, en tradingstation och ett avstånd.
        //Detta innebär att vi enbart skapar planeter när de upptäckts, och att vi inte skapar alla planeter i början av spelet.
        //3. Inom ramen för detta spel när vi enbart har 10 planeter är det kanske overkill, men om vi skulle vilja lägga till större mängder planeter är det ett smart val
        //då vi kan skapa planeterna "as we go", och inte kräva av kompilatorn att skapa tiotals/hundratals/tusentals planeter direkt.
        public static IEnumerable<Planet> GeneratePlanets(TradingStationFactory tradingStationFactory)
        {


            List<string> planetNames = new() { "Zephyria", "Bobo", "Pyralis", "Aquillon", "Astronia", "Terravox", "Luminara", "Dracoria", "Nebulon", "Celestria", "Volteron" };
            while (true)
            {
                if (planetNames.Count == 0)
                {
                    // Return the discovered planets ordered by closeness, skipping the current planet.
                    //För att effektivisera vår kod använder vi LINQ för att kunna sortera planeterna efter avstånd från nuvarande planet. Men vidare uppdatering hade krävt att vi använder oss av en annan datastruktur. Till exempel en heap.

                    //1. Nedan används yield.
                    //2. För varje planet som uppfyller kraven i LINQ-uttrycket, vill vi returnera en planet vilket vi gör genom yield.
                    //3. Vi använder yield eftersom vi returnerar en enumerable i funktionen. Vi använder också "yield break" om det är så att alla planeter från listan ovan är upptäckta och det inte finns några mer 
                    //planeter att skapa
                    foreach (var planet in discoveredPlanets.OrderBy(p => Math.Abs(p.Distance - currentPlanet.Distance)))
                    {
                        if (planet != currentPlanet)  // Ensure the current planet is not re-returned
                        {
                            yield return planet;
                        }
                    }
                    yield break;  // Exit the method once all discovered planets are returned
                }
                Random rnd = new();
                int index = rnd.Next(planetNames.Count);
                int baseDistance = 10;
                int distance = baseDistance + rnd.Next(5, 60);
                string planetName = planetNames[index];
                planetNames.RemoveAt(index);

                yield return new Planet(planetName, tradingStationFactory.createTradingStation(), distance);
            }
        }




        public static Planet TravelToAnotherPlanet(Planet currentPlanet, Player player, DisplayMenu menu)
        {

            //1. Här används LINQ
            //2. Vi använder LINQ för att kunna sortera planeterna efter avstånd från nuvarande planet.
            //3. Vi använder LINQ för att kunna ta de tre närmsta planeterna från nuvarande planet.

            //1. Vi använder här även Lambdas i funktionerna
            //2. Vi använder Lambdas tillsammans med LINQ där vi skapar korta funktioner för att kunna beräkna distanser mellan planeterna och returnera en lista av närmsta planeter.
            //3. Vi använder Lambdas för att kunna dra nytta av LINQ på ett enkelt och effektivt sätt, än att skapa massa funktioner som vi bara använder en gång.

            List<string> planetMenuOptions = closestPlanets.Select(p =>
            {
                int distance = Math.Abs(p.Distance - currentPlanet.Distance);
                return $"{p.Name} (Avstånd: {distance} parsecs, Bränslekostnad: {distance} ML)";
            }).ToList();

            planetMenuOptions.Add("Tillbaka till Huvudmenyn");

            int choice = menu.Menu($"Res till en annan planet - {currentPlanet.Name}", planetMenuOptions, player.Spaceship.FuelInfo());

            if (choice == planetMenuOptions.Count - 1)
            {
                return currentPlanet; // Return without changing planet if "Return to Main Menu" is selected
            }
            else
            {

                int distance = Math.Abs(closestPlanets[choice].Distance - currentPlanet.Distance);
                float fuelNeeded = distance / player.Spaceship.FuelEfficiency;

                if (player.Spaceship.Fuel >= fuelNeeded)
                {
                    player.Spaceship.ConsumeFuel(distance);
                    player.VisitedPlanets.Add(closestPlanets[choice]);
                    foreach (Planet planet in closestPlanets)
                    {
                        if (!discoveredPlanets.Contains(planet))
                        {
                            discoveredPlanets.Add(planet);
                        }
                    }
                    Planet travelledToPlanet = closestPlanets[choice];
                    if (discoveredPlanets.FirstOrDefault(p => p.Name == travelledToPlanet.Name) != null)
                    {
                        travelledToPlanet = discoveredPlanets.FirstOrDefault(p => p.Name == travelledToPlanet.Name);
                    }
                    if (travelledToPlanet.IsVisited == false)
                    {
                        travelledToPlanet.IsVisited = true;
                    }
                    closestPlanets.Clear();
                    closestPlanets = planets.Take(2).ToList();
                    closestPlanets.Add(currentPlanet);
                    turnCounter++;
                    turnOver = true;
                    return travelledToPlanet;
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



