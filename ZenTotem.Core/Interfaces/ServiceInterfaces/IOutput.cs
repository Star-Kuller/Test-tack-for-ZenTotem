namespace ZenTotem.Infrastructure;

/// <summary>
/// Allows you to do some additional output processing logic.
/// </summary>
public interface IOutput
{
    public void Send(string massange);
}