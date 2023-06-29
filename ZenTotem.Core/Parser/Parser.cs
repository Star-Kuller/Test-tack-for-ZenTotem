namespace ZenTotem.Core.Parser;

public class Parser : IParser
{
    private readonly ICommandRecognizerChain _recognizer;

    public Parser()
    {
        // Creating a command recognizer chain.
        _recognizer = new CommandRecognizerChain(
            new JsonCommand(),"-json");
        var deleteChainLink = new CommandRecognizerChain(
            new HelpCommand(),"-help");
    }
    
    public void Parse(string[] inputArguments)
    {
        try
        {
            var commandName = inputArguments[0].ToLower();
            var commandArguments = inputArguments.ToList();
            commandArguments.RemoveAt(0);
            
            _recognizer.ReturnCommand(commandName)
                .Execute(commandArguments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}