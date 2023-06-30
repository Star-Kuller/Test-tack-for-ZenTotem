using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetAllCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly ITableGenerator _tableGenerator;

    public GetAllCommand(IRepository repository, ITableGenerator tableGenerator)
    {
        _repository = repository;
        _tableGenerator = tableGenerator;
    }
    
    public void Execute(List<string> arguments)
    {
        var employees = _repository.GetAll();
        Console.WriteLine(_tableGenerator.Create(employees));
    }
}