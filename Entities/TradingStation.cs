using RymdRikedomar.Entities.Goods;

public class TradingStation
{
    public List<(IGood Good, int Stock)> AvailableGoods { get; set; }

    public TradingStation()
    {
        AvailableGoods = new List<(IGood, int)>
        {
            (new Spice(), 100),
            (new Metal(), 100)
        };
    }

    public (int PurchasePrice, int SellingPrice) FindPricesByName(string name)
    {
        var (Good, Stock) = AvailableGoods.FirstOrDefault(g => g.Good.Name == name);

        if (Good != null)
        {
            return (Good.PurchasePrice, Good.SellingPrice);
        }

        return (0, 0);
    }

    public int FindStockByName(string name)
    {
        var (Good, Stock) = AvailableGoods.FirstOrDefault(g => g.Good.Name == name);

        if (Good != null)
        {
            return Stock;
        }

        return 0;
    }
    public (IGood Good, int Stock)? FindGoodByName(string name)
    {
        return AvailableGoods.FirstOrDefault(g => g.Good.Name == name);
    }

    public void UpdateStock(IGood good, int newStock)
    {
        var item = AvailableGoods.FirstOrDefault(g => g.Good == good);
        if (item.Good != null)
        {
            AvailableGoods.Remove(item);
            AvailableGoods.Add((item.Good, newStock));
        }
    }
}