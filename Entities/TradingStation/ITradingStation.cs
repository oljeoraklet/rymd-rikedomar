using RymdRikedomar.Entities;
using RymdRikedomar.Entities.SpaceShip;


//Interfacet måste definiera dessa metoder då vi använder oss av dependency injection på alla planeter av typen ITradingStation.
//Detta innebär att i programklassen, när vi kallar på planeten och dess TradingStation, så måste vi implementera dessa metoder i detta interface.



//1. Composition over inheritence
//2. I och med detta interface möjliggör vi också ett Strategy Pattern då vi som ovan benämnt kan instansiera flera olika typer av TradingStations.
//3. Detta betyder att vi också genom detta premierar "composition over inheritence", eftersom vi komponerar en Planet med en TradingStation istället för att använda oss av arv. 
//Komposition möjliggör instansiering av olika typer av TradingStations som kan finnas på en planet.
//I nuvarande form är det enbart TradingStations med typer av varor som kan köpas och säljas, men man skulle i framtiden i och med detta interface enkelt
//kunna lägga till en ex. "Abandonded TradingStation" (som inte har någon funktion), eller en "Black Market TradingStation" (som säljer illegala varor).
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