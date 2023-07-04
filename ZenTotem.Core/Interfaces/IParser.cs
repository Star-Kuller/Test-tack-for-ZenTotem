namespace ZenTotem.Core;

/// <summary>
/// Gets the command name and selects the appropriate class. Many different implementations can be made.
/// </summary>
public interface IParser
{
    /// <summary>
    /// Allows you to pass a list of commands and their names.
    /// </summary>
    public Dictionary<string, ICommand> DictionaryCommands { get; set; }
    public ICommand Parse(string[] arguments);
}