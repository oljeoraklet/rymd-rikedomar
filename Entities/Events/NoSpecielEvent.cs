public class NoEvent
{

    public delegate void NoEventHandler(NoEvent randomEvent);

    public event NoEventHandler NoEventEvent;

    StringPrinter stringPrinter = new StringPrinter();
    public void OnRandomEvent(NoEvent noEvent)
    {
        if (NoEventEvent != null)
        {
            string msg = "Nothing special happened this day...";
            stringPrinter.Print(msg);
            Console.WriteLine("Press any key to continue...");
            NoEventEvent(noEvent);
        }
    }

}