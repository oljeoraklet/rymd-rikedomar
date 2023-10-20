class StringPrinter
{
    public void Print(string str)
    {
        foreach (char c in str)
        {
            Console.Write(c);
            Thread.Sleep(50);
        }
        Console.WriteLine();
        Console.WriteLine(" ");
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        Console.WriteLine(" ");
    }
}