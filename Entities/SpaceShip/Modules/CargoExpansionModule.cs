using RymdRikedomar.Entities.SpaceShip;
using RymdRikedomar.Entities.SpaceShip.Modules;

public class CargoExpansionModule : ISpaceshipModule
{
    public string Name { get { return "Cargo Expansion Module"; } }

    public string Usage => throw new NotImplementedException();

    public int PurchasePrice => throw new NotImplementedException();

    public int SellingPrice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
