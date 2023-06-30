using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetAllCommand : ICommand
{
    private IRepository _repository;

    public GetAllCommand(IRepository repository)
    {
        _repository = repository;
    }
    
    public void Execute(List<string> arguments)
    {
        Console.WriteLine();
    }
}