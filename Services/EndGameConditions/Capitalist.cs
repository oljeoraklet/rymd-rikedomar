using RymdRikedomar.Entities;

namespace RymdRikedomar.Services.EndGameConditions
{
    public class Capitalist : IEndGameCondition
    {
        public string ConditionName { get { return "Kapitalist"; } }
        public bool IsConditionMet(Player player)
        {
            //If player has 1000 credits return true else return false
            if (player.Units >= 100000)
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