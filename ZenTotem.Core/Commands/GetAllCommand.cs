using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetAllCommand : ICommand
{
    private readonly IRepository _repository;

    public GetAllCommand(IRepository repository)
    {
        _repository = repository;
    }
    
    public void Execute(List<string> arguments)
    {
        var employees = _repository.GetAll();
        foreach (var employee in employees)
        { 
            Console.WriteLine($"ID:{employee.Id} FN:{employee.FirstName} LN:{employee.LastName} S:{employee.Salary}");
        }
    }
}