using RymdRikedomar.Entities;

namespace RymdRikedomar.Services.EndGameConditions
{

    //Detta är ett interface som är en del av vårt ObserverPattern, och är en "Observer" i delen av detta pattern. 
    //Genom detta interface kan olika EndGameConditions implementeras. 
    //EndGameConditions notifieras av Player när en condition är uppfylld. 
    //Ett condition returnerar då en bool på att detta är uppfyllt, och spelet kan då ses som vunnet.
    //Vi använder detta för att kunna skapa olika typer av vinstvillkor för spelet och för att notifiera spelaren att denna har vunnit spelet, och på vilket sätt spelaren har vunnit. 
    public interface IEndGameCondition
    {
        string ConditionName { get; }
        bool IsConditionMet(Player player);
    }
}