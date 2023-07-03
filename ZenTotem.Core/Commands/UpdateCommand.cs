using ZenTotem.Core.Entities;
using ZenTotem.Core.Parser;
using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class UpdateCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IOutput _output;

    public UpdateCommand(IRepository repository, IOutput output)
    {
        _repository = repository;
        _output = output;
    }

    public void Execute(List<string> arguments)
    {
        if (arguments.Count < 2)
            throw new Exception("Error: Wrong number of arguments");
        
        if (!arguments[0].Contains("id:",StringComparison.InvariantCultureIgnoreCase))
            throw new Exception("Error: Invalid syntax");
        
        var allEmployees = _repository.GetAll();

        if (allEmployees.Count < 1)
            throw new Exception("Error: No employees in the file");

        if (!int.TryParse(arguments[0].Replace("id:", "",
                StringComparison.InvariantCultureIgnoreCase), out var id))
            throw new Exception("Error: Wrong id format");

        var employee = _repository.Get(id);

        foreach (var argument in arguments)
        {
            employee = PropertySetter.SetProperties(argument, employee);
        }

        if (string.IsNullOrEmpty(employee.FirstName))
            throw new Exception("Error: FirstName must be entered");

        _repository.Update(employee);
        _output.Send($"Updated {employee.Id}");
    }
}