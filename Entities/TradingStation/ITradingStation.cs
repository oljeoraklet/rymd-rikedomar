using RymdRikedomar.Entities;
using RymdRikedomar.Entities.SpaceShip;

public interface ITradingStation
{
    void BuyGoods(Player player);
    void SellGoods(Player player);
    void BuyFuel(Spaceship spaceship, Player player);
    void ShowFuelStatus(Spaceship spaceship);
}