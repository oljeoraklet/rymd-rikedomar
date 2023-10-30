using RymdRikedomar.Entities;

namespace RymdRikedomar.Services.EndGameConditions
{

    public interface IEndGameCondition
    {
        string ConditionName { get; }
        bool IsConditionMet(Player player);
    }
}