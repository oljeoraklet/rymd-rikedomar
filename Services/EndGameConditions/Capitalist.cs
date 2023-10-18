using RymdRikedomar.Entities;

public class Capitalist : IEndGameCondition
{
    public string ConditionName { get { return "Capitalist"; } }
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