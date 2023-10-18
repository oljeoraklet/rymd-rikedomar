public class NoEvent
{

    public delegate void NoEventHandler(NoEvent randomEvent);

    public event NoEventHandler NoEventEvent;

    public void OnRandomEvent(NoEvent noEvent)
    {
        if (NoEventEvent != null)
        {
            Console.WriteLine("Nothing special happened this day...");
            NoEventEvent(noEvent);
        }
    }
}