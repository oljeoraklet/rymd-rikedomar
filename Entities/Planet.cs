using RymdRikedomar.Entities.Goods;

namespace RymdRikedomar.Entities
{
    public class Planet
    {
        public string Name { get; set; }
        public List<IGood> AvailableGoods { get; private set; }

        public bool IsVisited { get; set; }

        public int XDistance { get; }

        public TradingStation TradingStation { get; set; }

        public Planet(string name, int xDistance)
        {
            Name = name;
            AvailableGoods = new();
            XDistance = xDistance;
            IsVisited = false;
            TradingStation = new();
        }

        // Methods can include: AddGood, RemoveGood, ChangePrice, etc.
    }

}