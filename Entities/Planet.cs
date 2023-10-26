using RymdRikedomar.Entities.Goods;
using SpaceConsoleMenu;

namespace RymdRikedomar.Entities
{
    public class Planet
    {
        public string Name { get; set; }
        public List<IGood> AvailableGoods { get; private set; }

        public bool IsVisited { get; set; }

        public int Distance;

        //Här använder vi Dependency Injection
        //Vi skapar en instans av interfaceet ITradingStation som vi sedan kan använda för att skapa en TradingStation
        //Vi gör detta för att vi vill kunna skapa olika typer av tradingstations för varje planet. 
        public ITradingStation TradingStation { get; set; }

        public Planet(string name, ITradingStation tradingStation, int distance)
        {
            Name = name;
            AvailableGoods = new();
            IsVisited = false;
            Distance = distance;
            TradingStation = tradingStation;
        }

        // Methods can include: AddGood, RemoveGood, ChangePrice, etc.

        double CalculateDemand()
        {
            Random random = new();
            double randomNumber = random.NextDouble() * (1.2 - 0.8) + 0.8;
            return randomNumber;
        }
    }


}