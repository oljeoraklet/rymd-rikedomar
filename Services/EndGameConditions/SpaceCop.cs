using RymdRikedomar.Entities;

namespace RymdRikedomar.Services.EndGameConditions
{
    public class SpaceCop : IEndGameCondition
    {
        public string ConditionName { get { return "Rymdsnut"; } }
        public bool IsConditionMet(Player player)
        {
            if (player.DefeatedPirates >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}