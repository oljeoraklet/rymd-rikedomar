using RymdRikedomar.Entities;

public class SpaceCop : IEndGameCondition
{
    public string ConditionName { get { return "Space Cop"; } }
    public bool IsConditionMet(Player player)
    {
        //If player has 1000 credits return true else return false
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