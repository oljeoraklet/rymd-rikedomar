using RymdRikedomar.Entities.Goods;

public class Fruit : IGood
{
    public string Name { get; set; } = "Frukt";

    public int PurchasePrice { get; set; }

    public int SellingPrice { get; set; }

    public void Update() { }

    public Fruit()
    {
        PurchasePrice = 30;
        SellingPrice = 10;

    }
}