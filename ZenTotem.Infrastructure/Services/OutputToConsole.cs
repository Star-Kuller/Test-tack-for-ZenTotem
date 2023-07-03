namespace ZenTotem.Infrastructure;

public class OutputToConsole : IOutput
{
    public void Send(string massange)
    {
        Console.WriteLine(massange);
    }
}