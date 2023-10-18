using RymdRikedomar.Entities;

namespace RymdRikedomar.Services.EndGameConditions
{
    public class Diplomat : IEndGameCondition
    {
        public string ConditionName { get { return "Diplomat"; } }
        public bool IsConditionMet(Player player)
        {
            //If player has visited 10 planets return true else return false
            return player.VisitedPlanets.Count >= 10;

        }
    }
}