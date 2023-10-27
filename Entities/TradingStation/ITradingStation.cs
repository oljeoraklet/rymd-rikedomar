using RymdRikedomar.Entities;
using RymdRikedomar.Entities.SpaceShip;


//Interfacet måste definiera dessa metoder då vi använder oss av dependency injection på alla planeter av typen ITradingStation.
//Detta innebär att i programklassen, när vi kallar på planeten och dess TradingStation, så måste vi implementera dessa metoder i detta interface.

//Detta interface möjliggör vid dependency injection i Planet-klassen, att kunna instansiera olika typer av TradingStations som kan finnas på en planet.
//I nuvarande form är det enbart TradingStations med typer av varor som kan köpas och säljas, men man skulle i framtiden i och med detta interface enkelt
//kunna lägga till en ex. "Abandonded TradingStation" (som inte har någon funktion), eller en "Black Market TradingStation" (som säljer illegala varor).

//Composition over inheritence
//I och med detta interface möjliggör vi också ett Strategy Pattern då vi som ovan benämnt kan instansiera flera olika typer av TradingStations.
//Detta betyder att vi också genom detta premierar "composition over inheritence", eftersom vi komponerar en Planet med en TradingStation istället för att använda oss av arv. 
public interface ITradingStation
{
    void BuyGoods(Player player);
    void SellGoods(Player player);
    void BuyFuel(Spaceship spaceship, Player player);
    void ShowFuelStatus(Spaceship spaceship);

    void IncreaseDemand(int x);

    void DecreaseDemand(int x);
}