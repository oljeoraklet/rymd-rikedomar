namespace RymdRikedomar.Entities.SpaceShip.Modules
{
    public interface ISpaceshipModule : IStoreItem
    {

        // Any shared methods from BaseSpaceshipModule would go here
        public string Usage { get; }
    }
}