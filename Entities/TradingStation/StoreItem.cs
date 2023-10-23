using RymdRikedomar.Entities;

namespace RymdRikedomar.Entities.TradingStation
{
    public class StoreItem<T> where T : IStoreItem
    {
        public T Item { get; set; }
        public int Stock { get; set; }

        public StoreItem(T item, int stock)
        {
            Item = item;
            Stock = stock;
        }

        public void BuyItem(int amount, Player player)
        {

            Stock -= amount;
            player.Units -= Item.PurchasePrice * amount;
            var item = player.Inventory.Find(i => i.Item.Name == Item.Name);
            if (item != null)
            {
                item.Stock += amount;
                return;
            }
            else
            {
                player.Inventory.Add(new StoreItem<IStoreItem>(Item, amount));
            }
        }
        public void SellItem(int amount, Player player)
        {
            Stock += amount;
            player.Units += Item.SellingPrice * amount;
            var item = player.Inventory.Find(i => i.Item.Name == Item.Name);
            if (item != null)
            {
                item.Stock -= amount;
                return;
            }
        }
    }

    public interface IStoreItem
    {
        string Name { get; }
        int PurchasePrice { get; }
        int SellingPrice
        {
            get; set;
        }
    }
}