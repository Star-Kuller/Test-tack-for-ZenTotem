using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetAllCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IOutputFormatter _outputFormatter;

    public GetAllCommand(IRepository repository, IOutputFormatter outputFormatter)
    {
        _repository = repository;
        _outputFormatter = outputFormatter;
    }
    
    public void Execute(List<string> arguments)
    {
        var employees = _repository.GetAll();
        Console.WriteLine(_outputFormatter.Create(employees));
    }
}