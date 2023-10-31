using RymdRikedomar.Entities.Goods;

public class DarkMatterFuelCells : IGood
{
    public string Name { get; set; } = "Bränsleceller av mörk materia";
    public int PurchasePrice { get; set; }

    public int SellingPrice { get; set; }

    public void Update() { }

    public DarkMatterFuelCells()
    {
        PurchasePrice = 500;
        SellingPrice = 500;

    }
}