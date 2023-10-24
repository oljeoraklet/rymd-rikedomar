using RymdRikedomar.Entities;



class PlanetFactory
{

    public static Planet CreatePlanet(string name, int xDistance, TradingStation<IStoreItem> tradingStation)
    {
        Planet planet = new Planet(name, xDistance, tradingStation);
        return planet;
    }
}