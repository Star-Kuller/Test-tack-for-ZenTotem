namespace ZenTotem.Core.Parser;

public class Parser : IParser
{
    private readonly ICommandRecognizerChain _recognizer;

    public Parser(ICommandRecognizerChain recognizer)
    {
        _recognizer = recognizer;
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