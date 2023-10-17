using RymdRikedomar.Entities.Goods;

public class Spice : IGood
{
    public string Name { get; set; } = "Spice";
    public int PurchasePrice { get; set; }
    public int SellingPrice { get; set; }
    public string Quality { get; set; }


    public Spice()
    {
        Random random = new Random();
        float randomNumber = (float)random.NextDouble();
        Console.WriteLine(randomNumber);
        Quality = "Fresh";
        CalculatePrice();
    }

    private void CalculatePrice()
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
    // Rest of the class remains unchanged
}