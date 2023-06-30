using ZenTotem.Core.Entities;
using ZenTotem.Core.Parser;
using ZenTotem.Infrastructure;

namespace ZenTotem.Core;
public class AddCommand : ICommand
{
    private readonly IRepository _repository;

    public AddCommand(IRepository repository)
    {
        _repository = repository;
    }

    public void Execute(List<string> arguments)
    {
        if (arguments.Count != 3)
            throw new Exception("Error: Wrong number of arguments");
        
        var allEmployees = _repository.GetAll();
        var nexId = 0;
        
        if (allEmployees.Count >= 1)
            nexId = allEmployees.OrderByDescending(e => e.Id).First().Id + 1;

        var employee = new Employee(nexId);

        foreach (var argument in arguments)
        {
            employee = PropertySetter.SetProperties(argument, employee);
        }

        if (string.IsNullOrEmpty(employee.FirstName))
            throw new Exception("Error: FirstName must be entered");

        _repository.Add(employee);
    }
}