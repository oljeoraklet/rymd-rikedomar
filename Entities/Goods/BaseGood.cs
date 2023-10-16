public abstract class BaseGood
{
    public string Name { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }

    // This being abstract means other goods will derive from this and might have specific properties or methods.
}