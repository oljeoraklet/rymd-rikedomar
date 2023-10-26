using RymdRikedomar.Entities;

public class MarketBoom
{

    public delegate void MarketBoomHandler(MarketBoom randomEvent);

    public event MarketBoomHandler MarketBoomEvent;

    public void OnRandomEvent(MarketBoom marketBoomEvent, List<Planet> planets)
    {
        //     if (marketBoomEvent != null)
        //     {
        //         string planetName = Planet(planets).Name;
        //         Console.WriteLine($"Efterfrågan går i taket på {planetName}!");
        //         Planet(planets).Demand *= 2;

        //         MarketBoomEvent(marketBoomEvent);
        //     }
        // }

        // public Planet Planet(List<Planet> planets)
        // {
        //     Random rnd = new Random();
        //     int random = rnd.Next(planets.Count);
        //     return planets[random];
    }
}