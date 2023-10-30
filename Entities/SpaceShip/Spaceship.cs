using RymdRikedomar.Entities.SpaceShip.Modules;
namespace RymdRikedomar.Entities.SpaceShip
{
    public class ModuleSlot : ISlot<ISpaceshipModule>
    {
        public ISpaceshipModule Module { get; private set; }

        public bool CanFit(ISpaceshipModule module)
        {
            // If the slot is empty, it can fit a module
            return Module == null;
        }

        public void AddModule(ISpaceshipModule module)
        {
            if (CanFit(module))
            {
                Module = module;
            }
            else
            {
                throw new InvalidOperationException("The slot is already occupied.");
            }
        }
    }
    public class Spaceship
    {
        public float Fuel { get; set; }
        public float FuelCapacity { get; set; }
        public float FuelEfficiency { get; set; }

        public int WeaponDamage { get; private set; }
        public int Health = 3;

        public List<ModuleSlot> Slots { get; private set; }

        private readonly IDefaultModuleProvider<ISpaceshipModule> DefaultModuleProvider;

        public class ModuleSlot : ISlot<ISpaceshipModule>
        {
            public ISpaceshipModule Module { get; private set; }

            public bool CanFit(ISpaceshipModule module)
            {
                // If the slot is empty, it can fit a module
                return Module == null;
            }

            public void AddModule(ISpaceshipModule module)
            {
                if (CanFit(module))
                {
                    Module = module;
                }
                else
                {
                    throw new InvalidOperationException("The slot is already occupied.");
                }
            }
        }

        public Spaceship(int numberOfSlots, IDefaultModuleProvider<ISpaceshipModule> defaultModuleProvider)
        {
            Fuel = 50;  // Default fuel
            FuelCapacity = 100;  // Default fuel capacity
            FuelEfficiency = 1;
            WeaponDamage = 0;
            Slots = new List<ModuleSlot>();
            DefaultModuleProvider = defaultModuleProvider;
            for (int i = 0; i < numberOfSlots; i++)
            {
                Slots.Add(new ModuleSlot()); // Default value for a list of ModuleSlots is null
            }
            ModuleSlot defaultModuleSlot = new ModuleSlot();
            defaultModuleSlot.AddModule(DefaultModuleProvider.ProvideDefaultModule());
            Slots.Add(defaultModuleSlot); // add the new ModuleSlot object with the default module to the list
        }

        public void ConsumeFuel(int distance)
        {
            float fuelNeeded = distance / FuelEfficiency;
            Fuel -= fuelNeeded;
        }

        // Further methods could be: Refuel, AddModule, etc.

        public void WeaponDamageUpgrade()
        {
            WeaponDamage += 1;
        }
        public string FuelInfo()
        {
            int percentage = (int)((float)Fuel / FuelCapacity * 100);
            int barSize = 50; // Size of the bar in console units
            int fuelAmount = percentage * barSize / 100;

            string fuelInfo = "[";

            Console.Write("[");
            for (int i = 0; i < barSize; i++)
            {
                if (i < fuelAmount)
                    fuelInfo += "█";  // Full block character to represent filled portion
                else
                    fuelInfo += " ";  // Space to represent unfilled portion
            }
            fuelInfo += $"] {percentage}/{FuelCapacity} ML\n";
            return fuelInfo;
        }

        public void UpdateSpaceShipModules()
        {
            foreach (var slot in Slots)
            {
                if (slot.Module != null && slot.Module is EngineModule)
                {
                    FuelEfficiency = 1.5f;
                }
                else if (slot.Module != null && slot.Module is WeaponModule)
                {
                    WeaponDamage = 1;
                }
            }
        }
    }
}