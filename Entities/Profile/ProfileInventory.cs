using RymdRikedomar.Entities.Goods;

public class ProfileInventory : IProfile<StoreItem<IStoreItem>>
{
    private List<StoreItem<IStoreItem>> items;

    public ProfileInventory(List<StoreItem<IStoreItem>> items)
    {
        this.items = items;
    }

    public List<StoreItem<IStoreItem>> GetItems()
    {
        return items;
    }

    public void ShowItems()
    {
        if (GetItems().Count == 0)
        {
            Console.WriteLine("Här var det tomt...");
        }
        foreach (var item in GetItems())
        {
            Console.WriteLine($"{item.Stock} - {item.Item.Name}");
        }
    }
}