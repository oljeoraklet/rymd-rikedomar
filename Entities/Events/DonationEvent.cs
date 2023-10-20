using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;

public class DonationEvent
{
    public delegate void DonationEventHandler(DonationEvent donationEvent);

    public event DonationEventHandler DonationEventEvent;

    public void OnRandomEvent(DonationEvent donationEvent, Planet currentPlanet, Player player)
    {

        StringPrinter stringPrinter = new StringPrinter();
        if (donationEvent != null)
        {
            string planetName = currentPlanet.Name;
            string msg = $"...bzzhh...bzzhhh...Traveler {player.Name}...We..need...bzzhhh...your..help..on..bzhhh...{planetName}...";
            stringPrinter.Print(msg);

            int rnd = RandomNumber(2);


            var goodTuple = RetrieveGood(player);
            int requestedAmount = goodTuple.Item1 / 2;
            IGood requestedGood = goodTuple.Item2;

            switch (rnd)
            {
                case 0:
                    string unitDonateMsg = $"...bzzhh...bzzhhh...We..need...bzzhhh...{player.Name}...to..donate..bzhhh...100 units...";
                    stringPrinter.Print(unitDonateMsg);
                    bool donateUnitsOrNot = HandleDonationInput();
                    if (donateUnitsOrNot)
                    {
                        player.Units -= 100;
                        player.influencePoints++;
                        Console.WriteLine($"You have donated 100 units to {planetName}");
                        Console.WriteLine("Press any key to continue...");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"You did not donate to {planetName}.");
                        Console.WriteLine("Press any key to continue...");
                        break;
                    }
                case 1:
                    string goodsDonateMsg = $"...bzzhh...bzzhhh...We..need...bzzhhh...you, {player.Name}...to..donate..bzhhh...{requestedAmount} {requestedGood}...";
                    stringPrinter.Print(goodsDonateMsg);
                    bool donateGoodsOrNot = HandleDonationInput();
                    if (donateGoodsOrNot)
                    {
                        RemoveFromInventory(requestedGood, requestedAmount, player);
                        player.influencePoints++;
                        Console.WriteLine($"You have donated {requestedAmount} {requestedGood} to {planetName}");
                        Console.WriteLine("Press any key to continue...");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"You did not donate anything to {planetName}.");
                        Console.WriteLine("Press any key to continue...");
                        break;
                    }
                default:
                    break;
            }

            DonationEventEvent(donationEvent);
        }
    }

    (int, IGood) RetrieveGood(Player player)
    {
        var goods = player.Inventory
            .OrderByDescending(goods => goods.Stock)
            .Select(goods => (goods.Stock, goods.Good));

        return goods.First();
    }

    public void RemoveFromInventory(IGood good, int amount, Player player)
    {
        var inventoryItem = player.Inventory.FirstOrDefault(item => item.Good == good);
        inventoryItem.Stock -= amount;
    }

    int RandomNumber(int num)
    {
        Random rnd = new Random();
        int random = rnd.Next(num);
        return random;
    }

    bool HandleDonationInput()
    {
        Console.WriteLine("Do you want to donate? (y/n)");
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
            Console.WriteLine("Invalid input, try again!");
            return HandleDonationInput();
        }
    }

}