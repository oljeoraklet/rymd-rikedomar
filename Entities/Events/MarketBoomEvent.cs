using RymdRikedomar.Entities;

public class MarketBoom
{

    public delegate void MarketBoomHandler(MarketBoom randomEvent);

    public event MarketBoomHandler MarketBoomEvent;


    Planet currentMarketBoom;
    public void OnRandomEvent(MarketBoom marketBoomEvent, List<Planet> planets)
    {

        if (currentMarketBoom != null)
        {
            currentMarketBoom.TradingStation.DecreaseDemand(2);
        }


        if (marketBoomEvent != null)
        {
            string planetName = Planet(planets).Name;
            Console.WriteLine($"Efterfrågan går i taket på {planetName}!");
            currentMarketBoom = Planet(planets);
            Planet(planets).TradingStation.IncreaseDemand(2);

            MarketBoomEvent(marketBoomEvent);
        }
    }

    public Planet Planet(List<Planet> planets)
    {
        Random rnd = new Random();
        int random = rnd.Next(planets.Count);
        return planets[random];
    }
}