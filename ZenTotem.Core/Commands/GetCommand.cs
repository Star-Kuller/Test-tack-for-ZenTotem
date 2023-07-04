using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IOutputFormatter _outputFormatter;
    private readonly IOutput _output;

    public GetCommand(IRepository repository, IOutputFormatter outputFormatter, IOutput output)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _outputFormatter = outputFormatter ?? throw new ArgumentNullException(nameof(outputFormatter));
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }
    
    public void Execute(List<string> arguments)
    {
        if (arguments.Count != 1)
            throw new Exception("Error: Wrong number of arguments");
        if (!arguments[0].Contains("id:"))
            throw new Exception("Error: Invalid syntax");
        
        if (!int.TryParse(arguments[0].Replace("id:", ""), out var id))
            throw new Exception("Error: Wrong id format");
        
        var employee = _repository.Get(id);
        
        var returned = _outputFormatter.CreateForOneObject(employee);
        _output.Send(returned);
    }
}