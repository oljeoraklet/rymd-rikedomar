using RymdRikedomar.Entities.SpaceShip;

namespace RymdRikedomar.Entities.SpaceShip.Modules
{
    public class WeaponModule : ISpaceshipModule
    {
        public string Name { get { return "Defense Shield Module"; } }

        public string Usage => throw new NotImplementedException();

        public int PurchasePrice => 12000;

        public int SellingPrice { get => 12000; set => throw new NotImplementedException(); }
    }

    public class DefaultWeaponProvider : IDefaultModuleProvider<WeaponModule>
    {
        public WeaponModule ProvideDefaultModule()
        {
            // Return a default engine module
            return new WeaponModule();
        }
    }
}