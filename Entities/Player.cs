using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip;
using RymdRikedomar.Services.EndGameConditions;

namespace RymdRikedomar.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Units { get; set; }
        public List<StoreItem<IStoreItem>> Inventory { get; private set; }
        public Spaceship Spaceship { get; set; }

        public List<Planet> VisitedPlanets;
        public int DefeatedPirates { get; set; }

        public int influencePoints = 0;



        public IEndGameCondition EndGameCondition { get; set; }

        public Player(string name)
        {
            Name = name;
            Units = 1000;  // Starting currency, can be adjusted
            Inventory = new List<StoreItem<IStoreItem>>();
            Spaceship = new Spaceship();  // Initialize with a basic spaceship
            EndGameCondition = new Diplomat();
            VisitedPlanets = new List<Planet>();
            DefeatedPirates = 0;
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