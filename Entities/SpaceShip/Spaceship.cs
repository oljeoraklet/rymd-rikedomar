using RymdRikedomar.Entities.SpaceShip.Modules;
namespace RymdRikedomar.Entities.SpaceShip
{
    public class Spaceship
    {
        public float Fuel { get; set; }

        public float FuelCapacity { get; set; }
        public int CargoCapacity { get; set; }
        public int FuelEfficiency { get; set; }

        public int WeaponDamage { get; private set; }

        public int Health = 3;
        public List<ISpaceshipModule> Modules { get; private set; }

        public Spaceship()
        {
            Fuel = 50;  // Default fuel
            FuelCapacity = 100;  // Default fuel capacity
            CargoCapacity = 50;  // Default cargo capacity
            FuelEfficiency = 1;
            WeaponDamage = 0;
            Modules = new List<ISpaceshipModule>();
        }

        public void ConsumeFuel(int distance)
        {
            int fuelNeeded = distance / FuelEfficiency;
            Fuel -= fuelNeeded;
        }

        // Further methods could be: Refuel, AddModule, etc.

        public void WeaponDamageUpgrade()
        {
            WeaponDamage += 1;
        }
    }
}