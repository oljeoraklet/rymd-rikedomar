using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip;

namespace RymdRikedomar.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Units { get; set; }
        public List<(IGood Good, int Stock)> Inventory { get; private set; }
        public Spaceship Spaceship { get; set; }

        public Player(string name)
        {
            Name = name;
            Units = 1000;  // Starting currency, can be adjusted
            Inventory = new List<(IGood, int)>
            {
                (new Spice(), 100),
                (new Metal(), 100)
        };
            Spaceship = new Spaceship();  // Initialize with a basic spaceship
        }

        public (int PurchasePrice, int SellingPrice) FindPricesByName(string name)
        {
            var good = Inventory.FirstOrDefault(g => g.Good.Name == name);

            if (good.Good != null)
            {
                return (good.Good.PurchasePrice, good.Good.SellingPrice);
            }

            return (0, 0);
        }

        public int FindStockByName(string name)
        {
            var good = Inventory.FirstOrDefault(g => g.Good.Name == name);

            if (good.Good != null)
            {
                return good.Stock;
            }

            return 0;
        }
        public (IGood Good, int Stock) FindGoodByName(string name)
        {
            return Inventory.FirstOrDefault(g => g.Good.Name == name);
        }

        public void UpdateStock(IGood good, int newStock)
        {
            var item = Inventory.Find(item => item.Item1.Name == good.Name);
            if (!item.Equals(default((IGood, int))))
            {
                Inventory.Remove(item);
                Inventory.Add((item.Item1, newStock));
            }
            Inventory.ForEach(i => Console.WriteLine(i.Item1.Name + " " + i.Item2));
        }

        // Additional methods can be added later, such as buying or selling goods.
    }

}