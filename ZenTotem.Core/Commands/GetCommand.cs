using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetCommand : ICommand
{
    private IRepository _repository;

    public GetCommand(IRepository repository)
    {
        _repository = repository;
    }
    
    public void Execute(List<string> arguments)
    {
        if (arguments.Count != 1)
            throw new Exception("Error: Wrong number of arguments");
        if (!arguments[0].Contains("id:"))
            throw new Exception("Error: Invalid syntax");
        
        if (!int.TryParse(arguments[0].Replace("id:", ""), out var id))
            throw new Exception("Error: Wrong format");
        
        var employee = _repository.Get(id);
        
        Console.WriteLine($"ID:{employee.Id} FN:{employee.FirstName} LN:{employee.LastName} S:{employee.Salary}");

    }
}