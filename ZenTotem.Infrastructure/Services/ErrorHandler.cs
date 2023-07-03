namespace ZenTotem.Infrastructure;

public class ErrorHandler : IErrorHandler
{
    private readonly ILogger? _logger;
    private readonly IOutput _output;

    public ErrorHandler(IOutput output, ILogger? logger = null)
    {
        _logger = logger;
        _output = output;
    }

    public void HandleError(Exception ex, string message)
    {
        var returnedMessage = "";
        
        if (message == "Error: File is empty")
            returnedMessage = FileEmpty();
        if (message == "Error: Unknown property")
            returnedMessage = UnknownProperty();
        if (message == "Error: Command not recognized")
            returnedMessage = CommandNotRecognized();
        if (message == "Error: Id cannot be less than 0")
            returnedMessage = IdLessZero();
        if (message == "Error: Wrong id format")
            returnedMessage = IdFormat();
        if (message == "Error: Wrong id format")
            returnedMessage = DecimalFormat();
        if (message == "Error: Wrong decimal format")
            returnedMessage = DecimalFormat();
        if (message == "Error: No employees in the file")
            returnedMessage = NoEmployees();
        if (message == "Error: Employee not found")
            returnedMessage = NoEmployees();
        if (message == "Error: Invalid syntax") 
            returnedMessage = InvalidSyntax();
        if (message == "Error: Wrong number of arguments") 
            returnedMessage = NumberOfArguments();
        if (message == "Error: FirstName must be entered") 
            returnedMessage = NoFirstName();
        
        if (returnedMessage == "")
            returnedMessage = Unrecognized(message);
        if(_logger is not null) 
            _logger.LogError(ex, message);
        
        _output.Send(returnedMessage);
    }

    private string FileEmpty() => "Nothing found in file.";
    private string UnknownProperty() => "The parameter could not be recognized.";
    private string CommandNotRecognized() => "The command is not recognized.\n\"-help\" - list of commands.";
    private string IdLessZero() => "ID must be greater than 0.";
    private string IdFormat() => "ID must be an integer.";
    private string DecimalFormat() => "The number must be entered in the format \"[integer],[fractional]\".";
    private string NoEmployees() => "Employee not found.";
    private string InvalidSyntax() => "You made a mistake in writing the command.";
    private string NumberOfArguments() => "This method has a different number of arguments.\nYou may have accidentally put a space between the ':'.";
    private string NoFirstName() => "The \"FirstName\" field is required.";
    private string Unrecognized(string m) => $"Failed to recognize error. \n {m}";
    
}