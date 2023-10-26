using RymdRikedomar.Entities.Goods;
using RymdRikedomar.Entities.SpaceShip.Modules;
using SpaceConsoleMenu;

class TradingStationFactory
{

    List<IStoreItemWrapper> exclusiveItems = new()
{
    new StoreItem<IGood>(new QuesarSilk(), 50),
    new StoreItem<IGood>(new DarkMatterFuelCells(), 20),
    new StoreItem<IGood>(new StellarCrystals(), 10),
};


    private IStoreItemWrapper randomizeStoreList()
    {
        Random random = new();
        int randomNumber = random.Next(0, 3);
        return exclusiveItems[randomNumber];
    }

    public TradingStation<IStoreItem> createTradingStation()
    {
        DisplayMenu displayMenu = new DisplayMenu();


        List<IStoreItemWrapper> tradingStationItems = new();
        tradingStationItems.Add(new StoreItem<IGood>(new Fruit(), 200));
        tradingStationItems.Add(new StoreItem<IGood>(new Spice(), 100));
        tradingStationItems.Add(new StoreItem<IGood>(new Metal(), 100));
        tradingStationItems.Add(new StoreItem<ISpaceshipModule>(new FuelEfficiencyModule(), 100));
        tradingStationItems.Add(randomizeStoreList());

        TradingStation<IStoreItem> tradingStation = new TradingStation<IStoreItem>(displayMenu, tradingStationItems);

        return tradingStation;

    }
}