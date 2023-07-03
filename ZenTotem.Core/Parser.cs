namespace ZenTotem.Core.Parser;

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