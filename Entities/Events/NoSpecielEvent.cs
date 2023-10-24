public class NoEvent
{

    public delegate void NoEventHandler(NoEvent randomEvent);

    public event NoEventHandler NoEventEvent;

    StringPrinter stringPrinter = new StringPrinter();
    public void OnRandomEvent(NoEvent noEvent)
    {
        if (NoEventEvent != null)
        {
            string msg = "Inget särskilt hände denna dag...";
            stringPrinter.Print(msg);
            Console.WriteLine("Tryck valfri tangent för att fortsätta...");
            NoEventEvent(noEvent);
        }
    }

}