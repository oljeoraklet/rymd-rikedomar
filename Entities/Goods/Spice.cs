using RymdRikedomar.Entities.Goods;

public class Spice : IGood
{
    public string Name { get; set; } = "Krydda";
    public int PurchasePrice { get; set; }
    public int SellingPrice { get; set; }
    public string Quality { get; set; }
    public void Update()
    {
        if (Quality == "Fresh")
        {
            PurchasePrice = 200;
            SellingPrice = 200;
        }
        else if (Quality == "Stale")
        {
            PurchasePrice = 100;
            SellingPrice = 75;
        }
        else if (Quality == "Rancid")
        {
            PurchasePrice = 50;
            SellingPrice = 40;
        }
        else
        {
            PurchasePrice = 20;
            SellingPrice = 20;
        }
    }


    public Spice()
    {
        Quality = "Fresh";
        Update();
    }


    // Rest of the class remains unchanged
}