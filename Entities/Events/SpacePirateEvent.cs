public class PirateEvent
{
    public delegate void PirateEventHandler(PirateEvent randomEvent);

    public event PirateEventHandler PirateEventEvent;

    StringPrinter stringPrinter = new StringPrinter();
    string pirateMsg1 = "Stop right there, explorer! You have been attacked by pirates!";
    string pirateMsg2 = "You have two options: fight or gives us materials!";

    void PirateHandler()
    {
        stringPrinter.Print(pirateMsg1);
        stringPrinter.Print(pirateMsg2);
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