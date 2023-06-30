using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly ITableGenerator _tableGenerator;

    public GetCommand(IRepository repository, ITableGenerator tableGenerator)
    {
        _repository = repository;
        _tableGenerator = tableGenerator;
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
        
        Console.WriteLine(_tableGenerator.CreateForOneRow(employee));

    }
}