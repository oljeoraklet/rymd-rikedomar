using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;

public class DonationEvent
{
    //1. Här använder vi en delegat
    //2. Vi skapar en delegat för att skapa en signatur för vår event-metod (DonationEventHandler) och sedan instansierar delegaten i DonationEventEvent
    //3. Detta gör det möjligt för andra klasser att ta del av detta event.
    public delegate void DonationEventHandler(DonationEvent donationEvent);

    public event DonationEventHandler DonationEventEvent;

    public void OnRandomEvent(DonationEvent donationEvent, Planet currentPlanet, Player player)
    {

        StringPrinter stringPrinter = new StringPrinter();

        string anyKeyMsg = "Tryck valfri tangent för att fortsätta....";

        if (donationEvent != null)
        {
            string planetName = currentPlanet.Name;
            string msg = $"...bzzhh...bzzhhh...Utforskare {player.Name}...Vi..behöver...bzzhhh...din..hjälp..på..bzhhh...{planetName}...";
            stringPrinter.Print(msg);

            int rnd = RandomNumber(2);

            if (player.Inventory.Count == 0)
            {
                string unitDonateMsg = $"...bzzhh...bzzhhh...Vi..behöver...bzzhhh...att du, {player.Name}..donerar..bzhhh...100 units...";
                stringPrinter.Print(unitDonateMsg);
                bool donateUnitsOrNot = HandleDonationInput();
                if (donateUnitsOrNot)
                {
                    player.Units -= 100;
                    player.influencePoints++;
                    Console.WriteLine($"Du har donerat 100 units till {planetName}");
                    Console.WriteLine(anyKeyMsg);

                }
                else
                {
                    Console.WriteLine($"Du valde att inte donera till {planetName}.");
                    Console.WriteLine(anyKeyMsg);
                }
            }
            else
            {


                var goodTuple = RetrieveGood(player);
                int requestedAmount = goodTuple.Item1 / 2;
                IGood requestedGood = goodTuple.Item2;


                switch (rnd)
                {
                    case 0:
                        string unitDonateMsg = $"...bzzhh...bzzhhh...Vi..behöver...bzzhhh...att du, {player.Name}..donerar..bzhhh...100 units...";
                        stringPrinter.Print(unitDonateMsg);
                        bool donateUnitsOrNot = HandleDonationInput();
                        if (donateUnitsOrNot)
                        {
                            player.Units -= 100;
                            player.influencePoints++;
                            Console.WriteLine($"Du har donerat 100 units till {planetName}");
                            Console.WriteLine(anyKeyMsg);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Du valde att inte donera till {planetName}.");
                            Console.WriteLine(anyKeyMsg);
                            break;
                        }
                    case 1:
                        string goodsDonateMsg = $"...bzzhh...bzzhhh...Vi..behöver...bzzhhh...att du, {player.Name}...donerar..bzhhh...{requestedAmount} {requestedGood}...";
                        stringPrinter.Print(goodsDonateMsg);
                        bool donateGoodsOrNot = HandleDonationInput();
                        if (donateGoodsOrNot)
                        {
                            RemoveFromInventory(requestedGood, requestedAmount, player);
                            player.influencePoints++;
                            Console.WriteLine($"Du har donerat {requestedAmount} {requestedGood} till {planetName}");
                            Console.WriteLine(anyKeyMsg);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Du valde att inte donera till {planetName}.");
                            Console.WriteLine(anyKeyMsg);
                            break;
                        }
                    default:
                        break;
                }
            }


            DonationEventEvent(donationEvent);
        }
    }

    (int, IGood) RetrieveGood(Player player)
    {
        var inventory = player.Inventory;
        int inventoryCount = inventory.Count;
        int randomIndex = RandomNumber(inventoryCount);
        var good = inventory[randomIndex].Item;
        int stock = inventory[randomIndex].Stock;
        return (stock, good as IGood);
    }

    public void RemoveFromInventory(IGood good, int amount, Player player)
    {
        var item = player.Inventory.Find(i => i.Item.Name == good.Name);
        if (item != null)
        {
            item.Stock -= amount;
            return;
        }
    }

    int RandomNumber(int num)
    {
        Random rnd = new Random();
        int random = rnd.Next(num);
        return random;
    }

    bool HandleDonationInput()
    {
        Console.WriteLine("Vill du donera? (y/n)");
        string input = Console.ReadLine();
        if (input == "y")
        {
            return true;
        }
        else if (input == "n")
        {
            return false;
        }
        else
        {
            Console.WriteLine("Fel input, försök igen!");
            return HandleDonationInput();
        }
    }

}