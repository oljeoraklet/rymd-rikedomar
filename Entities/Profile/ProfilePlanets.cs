using RymdRikedomar.Entities;

public class ProfileVisitedPlanets : IProfile<Planet>
{
    private List<Planet> planets;

    public ProfileVisitedPlanets(List<Planet> planets)
    {
        this.planets = planets;
    }

    public List<Planet> GetItems()
    {
        return planets;
    }
    public void ShowItems()
    {
        if (GetItems().Count == 0)
        {
            Console.WriteLine("Här var det tomt...");
        }
        foreach (var item in GetItems())
        {
            Console.WriteLine($"{item.Name}");
        }
    }
}


public class ProfileDiscoveredPlanets : IProfile<Planet>
{
    private List<Planet> planets;

    public ProfileDiscoveredPlanets(List<Planet> planets)
    {
        this.planets = planets;
    }

    public List<Planet> GetItems()
    {
        return planets;
    }

    public void ShowItems()
    {
        if (GetItems().Count == 0)
        {
            Console.WriteLine("Här var det tomt...");
        }
        foreach (var item in GetItems())
        {
            Console.WriteLine($"{item.Name}");
        }
    }
}