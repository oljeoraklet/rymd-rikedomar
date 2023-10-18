using RymdRikedomar.Entities;

public interface IEndGameCondition
{
    string ConditionName { get; }
    bool IsConditionMet(Player player);
}