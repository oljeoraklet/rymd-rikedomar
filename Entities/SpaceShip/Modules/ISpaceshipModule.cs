namespace RymdRikedomar.Entities.SpaceShip.Modules
{
    public interface ISpaceshipModule : IStoreItem
    {

        // Any shared methods from BaseSpaceshipModule would go here
        public string Usage { get; }
    }

    public interface ISlot<in T> where T : ISpaceshipModule
    {
        bool CanFit(T module);
        void AddModule(T module);
    }

    public interface IDefaultModuleProvider<out T> where T : ISpaceshipModule
    {
        T ProvideDefaultModule();
    }

}


