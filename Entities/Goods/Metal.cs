
using RymdRikedomar.Entities.Goods;

public class Metal : IGood
{
    public string Name { get; set; } = "Metal";
    public int PurchasePrice { get; set; }
    public int SellingPrice { get; set; }
    public string MetalType { get; set; }
    public decimal Purity { get; set; }

    public Metal()
    {
        PurchasePrice = 100;
        SellingPrice = 50;
        MetalType = "Iron";
        Purity = 0.5m;
    }

    // Rest of the class remains unchanged
}
