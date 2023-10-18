using RymdRikedomar.Entities;

public class Diplomat : IEndGameCondition
{
    public string ConditionName { get { return "Diplomat"; } }
    public bool IsConditionMet(Player player)
    {
        //If player has visited 10 planets return true else return false
        if (player.VisitedPlanets.Count >= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}