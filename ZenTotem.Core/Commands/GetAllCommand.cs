using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class GetAllCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IOutputFormatter _outputFormatter;
    private readonly IOutput _output;

    public GetAllCommand(IRepository repository, IOutputFormatter outputFormatter, IOutput output)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _outputFormatter = outputFormatter ?? throw new ArgumentNullException(nameof(outputFormatter));
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }
    
    public void Execute(List<string> arguments)
    {
        var employees = _repository.GetAll();
        var returned = _outputFormatter.CreateForList(employees);
        _output.Send(returned);
    }
}