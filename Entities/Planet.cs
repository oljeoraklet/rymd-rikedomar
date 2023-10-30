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

        //1. Här använder vi Dependency Injection
        //2. Vi skapar en instans av interfacet ITradingStation som vi sedan kan använda för att skapa en TradingStation (som genom dependency injection kan inneha olika beteenden)
        //3. Vi gör detta för att vi vill kunna skapa olika typer av tradingstations för varje planet. 

        //1. Här använder i ett "Strategy Pattern"
        //2. Kontexten här är planeten, då det är här vi vill skapa vår tradinstation. Vår strategi är ITradingStation, samt de konkreta strategierna är de olika tradingstations som skapas via dependency injection.
        //3. Vi vill använda oss av detta pattern för att ha möjligheten att skapa olika typer av tradingstations för varje planet.
        public ITradingStation TradingStation { get; set; }

        public Planet(string name, ITradingStation tradingStation, int distance)
        {
            Name = name;
            AvailableGoods = new();
            IsVisited = false;
            Distance = distance;
            TradingStation = tradingStation;
        }

        double CalculateDemand()
        {
            Random random = new();
            double randomNumber = random.NextDouble() * (1.2 - 0.8) + 0.8;
            return randomNumber;
        }
    }


}