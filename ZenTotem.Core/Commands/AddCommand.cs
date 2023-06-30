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

        var maxId = _repository.AsyncGetAll().Result.OrderByDescending(e => e.Id).First().Id;
        var employee = new Employee(maxId + 1);

        foreach (var argument in arguments)
        {
            switch (argument.Split(':')[0])
            {
                case "firstname":
                    employee.FirstName = argument.Replace("FirstName:", "");
                    break;
                case "LastName":
                    employee.LastName = argument.Replace("LastName:", "");
                    break;
                case "Salary":
                    employee.Salary = Convert.ToDecimal(argument.Replace("Salary:", ""));
                    break;
                default:
                    throw new Exception("Error: Unknown property");
            }
        }

        if (string.IsNullOrEmpty(employee.FirstName))
            throw new Exception("Error: FirstName must be entered");
        if (decimal.IsCanonical(employee.Salary))
            throw new Exception("Error: Salary must be entered");

        _repository.AsyncAdd(employee);
    }
}