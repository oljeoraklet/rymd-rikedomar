using RymdRikedomar.Entities.SpaceShip;

namespace RymdRikedomar.Entities.SpaceShip.Modules
{
    public class EngineModule : ISpaceshipModule
    {
        public string Name { get { return "Basic Engine Module"; } }

        public string Usage => throw new NotImplementedException();

        public int PurchasePrice => 10000;

        int IStoreItem.SellingPrice { get => 10; set => throw new NotImplementedException(); }
    }
    public class DefaultEngineProvider : IDefaultModuleProvider<EngineModule>
    {
        public EngineModule ProvideDefaultModule()
        {
            // Return a default engine module
            return new EngineModule();
        }
    }
}