using RymdRikedomar.Entities.Goods;

public class StellarCrystals : IGood
{
    public string Name { get; set; } = "Stellära kristaller";

    public int PurchasePrice { get; set; }

    public int SellingPrice { get; set; }

    public void Update() { }

    public StellarCrystals()
    {
        PurchasePrice = 1000;
        SellingPrice = 900;

    }
}