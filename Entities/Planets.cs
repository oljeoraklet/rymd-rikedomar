using System.Dynamic;
using RymdRikedomar.Entities;
using RymdRikedomar.Entities.Goods;

public class Planets
{
    private List<Planet> planets = new List<Planet>();

    public Planets()
    {
        CreatePlanet("Zephyria", 0);
        CreatePlanet("Aquillon", 2);
        CreatePlanet("Pyralis", 5);
        CreatePlanet("Astronia", 6);
        CreatePlanet("Terravox", 8);
        CreatePlanet("Luminara", 11);
        CreatePlanet("Dracoria", 12);
        CreatePlanet("Nebulon", 15);
        CreatePlanet("Celestria", 17);
        CreatePlanet("Volteron", 18);
    }

    private void CreatePlanet(string name, int xDistance)
    {
        planets.Add(new Planet(name, xDistance));
    }

}

