public class Spice : BaseGood
{
    // Additional properties specific to Spice can go here.
    // Example: Quality, Rarity, etc.
    public string Quality { get; set; }  // e.g., "Premium", "Standard", "Low Grade"

    public Spice()
    {
        Name = "Spice";
        // You can set default prices, but it's better to set them dynamically based on game conditions.
        PurchasePrice = 100;  // Example price
        SellingPrice = 120;  // Example price
    }

    // Any additional methods specific to Spice can be added.
}
