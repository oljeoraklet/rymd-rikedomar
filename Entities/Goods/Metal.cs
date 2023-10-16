public class Metal : BaseGood
{
    // Additional properties specific to Metal can go here.
    // Example: Type (Iron, Gold, etc.), Purity, Weight, etc.
    public string MetalType { get; set; }  // e.g., "Iron", "Gold", "Copper"
    public decimal Purity { get; set; }  // e.g., 0.99 for 99% pure

    public Metal()
    {
        Name = "Metal";
        // Again, setting default prices for demonstration, but it's better to set them dynamically.
        PurchasePrice = 50;  // Example price
        SellingPrice = 65;  // Example price
    }

    // Any additional methods specific to Metal can be added.
}
