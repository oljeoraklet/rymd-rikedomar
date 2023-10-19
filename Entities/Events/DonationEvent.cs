using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;

public class DonationEvent
{
    public delegate void DonationEventHandler(DonationEvent donationEvent);

    public event DonationEventHandler DonationEventEvent;

    public void OnRandomEvent(DonationEvent donationEvent, Planet currentPlanet, Player player)
    {
        if (donationEvent != null)
        {
            string planetName = currentPlanet.Name;
            Console.WriteLine($"...bzzhh...bzzhhh...Traveler {player.Name}...We..need...bzzhhh...your..help..on..bzhhh...{planetName}...");

            int rnd = RandomNumber(2);

            var goodTuple = RetrieveGood(player);
            int requestedAmount = goodTuple.Item1 / 2;
            IGood requestedGood = goodTuple.Item2;

            switch (rnd)
            {
                case 0:
                    Console.WriteLine($"...bzzhh...bzzhhh...We..need...bzzhhh...{player.Name}...to..donate..bzhhh...100 units...");
                    player.Units -= 100;
                    break;
                case 1:
                    Console.WriteLine($"...bzzhh...bzzhhh...We..need...bzzhhh...you, {player.Name}...to..donate..bzhhh...{requestedAmount} {requestedGood}...");
                    RemoveFromInventory(requestedGood, requestedAmount, player);
                    player.influencePoints++;
                    break;
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
}