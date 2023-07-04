namespace ZenTotem.Core;

/// <summary>
/// Common interface for all commands.
/// </summary>
public interface ICommand
{
    public void Execute(List<string> arguments);
}