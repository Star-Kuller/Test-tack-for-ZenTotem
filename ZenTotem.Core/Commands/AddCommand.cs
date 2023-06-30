using ZenTotem.Core.Entities;
using ZenTotem.Infrastructure;

namespace ZenTotem.Core;
public class AddCommand : ICommand
{
    private IRepository _repository;

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
            switch (argument.Split(':')[0])
            {
                case "FirstName":
                    employee.FirstName = argument.Replace("FirstName:", "");
                    break;
                case "LastName":
                    employee.LastName = argument.Replace("LastName:", "");
                    break;
                case "Salary":
                    var salary = argument.Replace("Salary:", "");
                    if (!decimal.TryParse(salary, out var d))
                        throw new Exception("Error: Wrong format");
                    employee.Salary = d;
                    break;
                default:
                    throw new Exception("Error: Unknown property");
            }
        }

        if (string.IsNullOrEmpty(employee.FirstName))
            throw new Exception("Error: FirstName must be entered");

        _repository.Add(employee);
    }
}