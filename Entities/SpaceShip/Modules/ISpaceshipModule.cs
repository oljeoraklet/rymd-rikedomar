namespace RymdRikedomar.Entities.SpaceShip.Modules
{
    public interface ISpaceshipModule : IStoreItem
    {

        // Any shared methods from BaseSpaceshipModule would go here
        public string Usage { get; }
    }


    //Användning av contravarians
    //För att kunna använda olika typer av moduler i samma slot
    //Detta gör att vi kan använda olika typer av moduler i samma slot
    public interface ISlot<in T> where T : ISpaceshipModule
    {
        bool CanFit(T module);
        void AddModule(T module);
    }

    //Use of covarians in order to be able to return different types of modules
    //This makes it possible to return different types of modules
    public interface IDefaultModuleProvider<out T> where T : ISpaceshipModule
    {
        T ProvideDefaultModule();
    }

}


