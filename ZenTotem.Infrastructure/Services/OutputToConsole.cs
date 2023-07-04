namespace ZenTotem.Infrastructure;

/// <summary>
/// This class allows you to do some additional output processing logic.
/// </summary>
public class OutputToConsole : IOutput
{
    public void Send(string massange) => Console.WriteLine(massange);
    
}