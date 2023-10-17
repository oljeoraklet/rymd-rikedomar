using RymdRikedomar.Entities.Goods;

namespace RymdRikedomar.Entities
{
    public class Planet
    {
        public string Name { get; set; }
        public List<IGood> AvailableGoods { get; private set; }

        public bool isVisited { get; set; }

        public int xDistance { get; }

        public Planet(string name, int xDistance)
        {
            Name = name;
            AvailableGoods = new List<IGood>();
            this.xDistance = xDistance;
            isVisited = false;
        }

        // Methods can include: AddGood, RemoveGood, ChangePrice, etc.
    }

}