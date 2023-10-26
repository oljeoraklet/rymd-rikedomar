using RymdRikedomar.Entities.Goods;
using SpaceConsoleMenu;

class TradingStationFactory
{

    List<StoreItem<IStoreItem>> exclusiveItems = new() {
        new StoreItem<IStoreItem>(new QuesarSilk(), 50),
        new StoreItem<IStoreItem>(new DarkMatterFuelCells(), 20),
        new StoreItem<IStoreItem>(new StellarCrystals(), 10),
    };


    private StoreItem<IStoreItem> randomizeStoreList()
    {
        Random random = new();
        int randomNumber = random.Next(0, 3);
        return exclusiveItems[randomNumber];
    }

    double CalculateDemand()
    {
        Random random = new();
        double randomNumber = random.NextDouble() * (1.2 - 0.8) + 0.8;
        return randomNumber;
    }

    public TradingStation<IStoreItem> createTradingStation()
    {
        DisplayMenu displayMenu = new DisplayMenu();


        List<StoreItem<IStoreItem>> tradingStationItems = new();
        tradingStationItems.Add(new StoreItem<IStoreItem>(new Fruit(), 200));
        tradingStationItems.Add(new StoreItem<IStoreItem>(new Spice(), 100));
        tradingStationItems.Add(new StoreItem<IStoreItem>(new Metal(), 100));
        tradingStationItems.Add(randomizeStoreList());

        TradingStation<IStoreItem> tradingStation = new TradingStation<IStoreItem>(displayMenu, tradingStationItems, CalculateDemand());

        return tradingStation;

    }
}