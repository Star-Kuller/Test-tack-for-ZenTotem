using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class DeleteCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IOutput _output;

    public DeleteCommand(IRepository repository, IOutput output)
    {
        _repository = repository;
        _output = output;
    }
    
    public void Execute(List<string> arguments)
    {
        if (arguments.Count != 1)
            throw new Exception("Error: Wrong number of arguments");
        if (!arguments[0].Contains("id:"))
            throw new Exception("Error: Invalid syntax");
        
        var allEmployees = _repository.GetAll();

        if (allEmployees.Count < 1)
            throw new Exception("Error: No employees in the file");

        if (!int.TryParse(arguments[0].Replace("id:", ""), out var deleteId))
            throw new Exception("Error: Wrong id format");
        
        _repository.Delete(deleteId);
        _output.Send($"Deleted employee {deleteId}");
    }
}