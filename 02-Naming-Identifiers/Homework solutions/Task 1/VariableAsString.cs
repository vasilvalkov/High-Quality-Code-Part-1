public class Startup
{
    public const int MAX_COUNT = 6;
    
    public static void Main()
    {
        ConsoleWriter writer = new ConsoleWriter();

        writer.StringifyBoolean(true);
    }
}

internal class ConsoleWriter
{
    public void StringifyBoolean(bool value)
    {
        string booleanAsString = value.ToString();

        Console.WriteLine(booleanAsString);
    }
}