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
            PurchasePrice = 100;
            SellingPrice = 200;
        }
        else if (Quality == "Stale")
        {
            PurchasePrice = 50;
            SellingPrice = 100;
        }
        else if (Quality == "Rancid")
        {
            PurchasePrice = 25;
            SellingPrice = 50;
        }
        else
        {
            PurchasePrice = 10;
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