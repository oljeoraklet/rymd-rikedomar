using RymdRikedomar.Entities.Goods;
using SpaceConsoleMenu;

namespace RymdRikedomar.Entities
{
    public class Planet
    {
        public string Name { get; set; }
        public List<IGood> AvailableGoods { get; private set; }

        public bool IsVisited { get; set; }

        public int XDistance { get; }

        public double Demand { get; set; }

        public double Supply { get; private set; }

        public TradingStation TradingStation { get; set; }

        public Planet(string name, int xDistance)
        {
            Name = name;
            AvailableGoods = new();
            XDistance = xDistance;
            IsVisited = false;
            Demand = CalculateDemand();
            Supply = Demand + 0.3;
            TradingStation = new(new DisplayMenu());
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