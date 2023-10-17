public abstract class BaseGood
{
    public string Name { get; set; }
    public int PurchasePrice { get; set; }
    public int SellingPrice { get; set; }

    // This being abstract means other goods will derive from this and might have specific properties or methods.
}