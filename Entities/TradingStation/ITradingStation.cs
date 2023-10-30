using RymdRikedomar.Entities;
using RymdRikedomar.Entities.SpaceShip;

public interface ITradingStation
{
    void BuyGoods(Player player);
    void SellGoods(Player player);
    void BuyFuel(Player player);

    void BuyModules(Player player);
    void ShowFuelStatus(Player player);

    void IncreaseDemand(int x);

    void DecreaseDemand(int x);
}