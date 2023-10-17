using RymdRikedomar.Entities.SpaceShip.Modules;
namespace RymdRikedomar.Entities.SpaceShip
{
    public class Spaceship
    {
        public decimal Fuel { get; set; }
        public int CargoCapacity { get; set; }
        public decimal FuelEfficiency { get; set; }
        public List<ISpaceshipModule> Modules { get; private set; }

        public Spaceship()
        {
            Fuel = 100;  // Default fuel
            CargoCapacity = 50;  // Default cargo capacity
            FuelEfficiency = 1;
            Modules = new List<ISpaceshipModule>();
        }

        // Further methods could be: Refuel, AddModule, etc.
    }
}