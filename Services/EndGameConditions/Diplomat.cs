using RymdRikedomar.Entities;

namespace RymdRikedomar.Services.EndGameConditions
{
    public class Diplomat : IEndGameCondition
    {
        public string ConditionName { get { return "Diplomat"; } }
        public bool IsConditionMet(Player player)
        {
            return player.influencePoints >= 5;

        }
    }
}