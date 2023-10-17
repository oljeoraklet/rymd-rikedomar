using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip;

namespace RymdRikedomar.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public decimal Currency { get; set; }
        public List<IGood> Inventory { get; private set; }
        public Spaceship Spaceship { get; set; }

        public Player(string name)
        {
            Name = name;
            Currency = 1000;  // Starting currency, can be adjusted
            Inventory = new List<IGood>();
            Spaceship = new Spaceship();  // Initialize with a basic spaceship
        }

        // Additional methods can be added later, such as buying or selling goods.
    }
}