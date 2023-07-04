namespace ZenTotem.Core.Parser;

/// <summary>
/// Gets the command name and selects the appropriate class. Many different implementations can be made.
/// </summary>
public class Parser : IParser
{
    public Dictionary<string, ICommand> DictionaryCommands { get; set; }

    public ICommand Parse(string[] inputArguments)
    {
        var commandName = inputArguments[0].ToLower();
        DictionaryCommands.TryGetValue(commandName, out var command);
        if (command is null)
            throw new Exception("Error: Command not recognized");
        return command;
    }
}