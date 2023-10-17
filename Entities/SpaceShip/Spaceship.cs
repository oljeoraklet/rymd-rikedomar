using RymdRikedomar.Entities.SpaceShip.Modules;
namespace RymdRikedomar.Entities.SpaceShip
{
    public class Spaceship
    {
        public float Fuel { get; set; }
        public int CargoCapacity { get; set; }
        public int FuelEfficiency { get; set; }
        public List<ISpaceshipModule> Modules { get; private set; }

        public Spaceship()
        {
            Fuel = 10;  // Default fuel
            CargoCapacity = 50;  // Default cargo capacity
            FuelEfficiency = 1;
            Modules = new List<ISpaceshipModule>();
        }

        public void ConsumeFuel(int distance)
        {
            int fuelNeeded = distance / FuelEfficiency;
            Fuel -= fuelNeeded;
        }

        // Further methods could be: Refuel, AddModule, etc.
    }
}