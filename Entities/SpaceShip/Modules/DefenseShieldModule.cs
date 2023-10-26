using RymdRikedomar.Entities.SpaceShip;

namespace RymdRikedomar.Entities.SpaceShip.Modules
{
    public class DefenseShieldModule : ISpaceshipModule
    {
        public string Name { get { return "Defense Shield Module"; } }

        public string Usage => throw new NotImplementedException();

        public int PurchasePrice => throw new NotImplementedException();

        public int SellingPrice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}