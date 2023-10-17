
using RymdRikedomar.Entities.Goods;

public class Fuel : IGood
{
    public string Name { get; set; } = "Fuel";
    public int PurchasePrice { get; set; }
    public int SellingPrice { get; set; }
    public decimal Efficiency { get; set; }
    public void Update() { }


    // Rest of the class remains unchanged
}
