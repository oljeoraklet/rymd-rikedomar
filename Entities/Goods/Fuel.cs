public class Fuel : BaseGood
{
    // Additional properties specific to Fuel can go here.
    // Example: Efficiency, which might determine how far the spaceship can travel using a unit of this fuel.
    public decimal Efficiency { get; set; }  // e.g., 1.2 for 120% standard efficiency

    public Fuel()
    {
        Name = "Fuel";
        // Default prices for demonstration, but you might set them dynamically in a real game scenario.
        PurchasePrice = 20;  // Example price
        SellingPrice = 25;  // Example price
    }

    // Any additional methods specific to Fuel can be added.
    // For instance, you might have a method to calculate how far a spaceship can travel using this fuel.
}
