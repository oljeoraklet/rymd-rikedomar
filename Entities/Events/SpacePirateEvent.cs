public class PirateEvent
{
    public delegate void PirateEventHandler(PirateEvent randomEvent);

    public event PirateEventHandler PirateEventEvent;

    void PirateHandler()
    {
        Console.WriteLine("Stop right there, explorer! You have been attacked by pirates!");
        Console.WriteLine("You have two options: fight or gives us materials!");
    }

    public void OnRandomEvent(PirateEvent pirateEvent)
    {
        if (PirateEventEvent != null)
        {
            PirateHandler();
            PirateEventEvent(pirateEvent);
        }
    }
}