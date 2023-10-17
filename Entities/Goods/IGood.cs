namespace RymdRikedomar.Entities.Goods
{
    public interface IGood
    {
        string Name { get; set; }
        int PurchasePrice { get; }
        int SellingPrice { get; }
        void Update();

        // If there were any methods in BaseGood, they would go here, e.g.:
        // void SomeMethod();
    }
}