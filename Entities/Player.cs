using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip;
using RymdRikedomar.Services.EndGameConditions;

namespace RymdRikedomar.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Units { get; set; }
        public List<(IGood Good, int Stock)> Inventory { get; private set; }
        public Spaceship Spaceship { get; set; }

        public List<Planet> VisitedPlanets;
        public int DefeatedPirates { get; set; }

        public int influencePoints = 0;

        public int Health = 3;

        public IEndGameCondition EndGameCondition { get; set; }

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
            EndGameCondition = new Diplomat();
            VisitedPlanets = new List<Planet>();
            DefeatedPirates = 0;
        }

        public (int PurchasePrice, int SellingPrice) FindPricesByName(string name)
        {
            var (Good, Stock) = Inventory.FirstOrDefault(g => g.Good.Name == name);

            if (Good != null)
            {
                return (Good.PurchasePrice, Good.SellingPrice);
            }

            return (0, 0);
        }

        public int FindStockByName(string name)
        {
            var (Good, Stock) = Inventory.FirstOrDefault(g => g.Good.Name == name);

            if (Good != null)
            {
                return Stock;
            }

            return 0;
        }
        public (IGood Good, int Stock) FindGoodByName(string name)
        {
            return Inventory.FirstOrDefault(g => g.Good.Name == name);
        }

        public void UpdateStock(IGood good, int newStock)
        {
            var item = Inventory.Find(item => item.Good.Name == good.Name);
            if (!item.Equals(default((IGood, int))))
            {
                Inventory.Remove(item);
                Inventory.Add((item.Item1, newStock));
            }
            Inventory.ForEach(i => Console.WriteLine(i.Good.Name + " " + i.Item2));
        }

        public bool CheckIfEndConditionsAreMet()
        {
            return EndGameCondition.IsConditionMet(this);
        }

        // Additional methods can be added later, such as buying or selling goods.

        //Methods to subscribe to events

        public void MarketBoomEventHandler(MarketBoom marketBoomEvent) { }
        public void DonationEventHandler(DonationEvent donationEvent) { }
        public void PirateEventHandler(PirateEvent pirateEvent) { }
        public void NoEventHandler(NoEvent randomEvent) { }
    }

}