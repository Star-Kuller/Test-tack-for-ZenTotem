namespace ZenTotem.Core;

public class HelpCommand : ICommand
{
    private const string HelpMessage =
        "-help - Prints a list of commands to the console.\n" +
        "-json path:{path} [name:{name}] - Specifies the path to the json file or creates one.\n" +
        "WIP";
    public void Execute(List<string> arguments)
    {
        Console.WriteLine(HelpMessage);
    }
}