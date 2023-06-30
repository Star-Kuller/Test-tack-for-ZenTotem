using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class DeleteCommand : ICommand
{
    private IRepository _repository;

    public DeleteCommand(IRepository repository)
    {
        _repository = repository;
    }
    
    public void Execute(List<string> arguments)
    {
        if (arguments.Count != 1)
            throw new Exception("Error: Wrong number of arguments");
        if (!arguments[0].Contains("id:"))
            throw new Exception("Error: Invalid syntax");
        
        var allEmployees = _repository.GetAll();

        if (allEmployees.Count < 1)
            throw new Exception("Error: There are no employees in the file");

        if (!int.TryParse(arguments[0].Replace("id:", ""), out var deleteId))
            throw new Exception("Error: Wrong format");
        
        _repository.Delete(deleteId);
    }
}