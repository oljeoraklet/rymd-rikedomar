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