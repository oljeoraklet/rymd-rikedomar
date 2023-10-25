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

        public bool hasWon = false;

        public IEndGameCondition winningCondition { get; private set; }

        private List<IEndGameCondition> endGameCondtions;


        public Player(string name, List<IEndGameCondition> endGameConditions)
        {
            Name = name;
            Units = 1000;  // Starting currency, can be adjusted
            Inventory = new List<StoreItem<IStoreItem>>();
            Spaceship = new Spaceship();  // Initialize with a basic spaceship
            VisitedPlanets = new List<Planet>();
            this.endGameCondtions = endGameConditions;
            DefeatedPirates = 0;
        }



        //Methods to subscribe to events


        //Här använder vi events
        //Vi skapar en metod för varje event som vi vill prenumerera på
        //Vi använder events för att vi vill at vår player ska kunna få ta del av events som sker i spelets gång. 
        public void MarketBoomEventHandler(MarketBoom marketBoomEvent) { }
        public void DonationEventHandler(DonationEvent donationEvent) { }
        public void PirateEventHandler(PirateEvent pirateEvent) { }
        public void NoEventHandler(NoEvent randomEvent) { }


        //Här använder vi en metod för att uppdatera alla observers i vårt ObserverPattern
        public void notifyConditions()
        {

            foreach (IEndGameCondition condition in endGameCondtions)
            {
                bool conditionIsMet = condition.IsConditionMet(this);

                if (conditionIsMet)
                {
                    winningCondition = condition;
                    hasWon = true;
                }
            }
        }


    }

}