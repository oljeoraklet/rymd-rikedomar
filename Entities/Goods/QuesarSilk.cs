using RymdRikedomar.Entities.Goods;

public class QuesarSilk : IGood
{
    public string Name { get; set; } = "Kvasarsilke";

    public int PurchasePrice { get; set; }

    public int SellingPrice { get; set; }

    public void Update() { }

    public QuesarSilk()
    {
        PurchasePrice = 250;
        SellingPrice = 250;
    }
}