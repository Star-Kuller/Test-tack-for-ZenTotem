namespace ZenTotem.Core;

public interface IParser
{
    public Dictionary<string, ICommand> DictionaryCommands { get; set; }
    public ICommand Parse(string[] arguments);
}