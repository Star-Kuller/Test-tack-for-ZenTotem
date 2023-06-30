using Microsoft.Extensions.Configuration;
using ZenTotem.Core;
using ZenTotem.Core.Parser;

namespace ZenTotem.Infrastructure;
public class DiContainer
{
    private static DiContainer? _diContainer;

    private IRepository _repository;
    private IParser _parser;
    private ICommandRecognizerChain _recognizer;

    // Singleton.
    private DiContainer(){}
    public static DiContainer GetContainer()
    {
        if (_diContainer == null)
            _diContainer = new DiContainer();
        return _diContainer;
    }
    
    
    public IParser GetParser() => _parser;
    
    public void Configure(IConfigurationRoot configuration)
    {

        _repository = new JsonRepository(configuration["jsonFilePath"]);
        
        // Command recognizer chain.
        // It is recommended to set up in from the most rarest team to the frequent.
        var jsonChainLink = new CommandRecognizerChain(
            new JsonCommand(),"-json", null);
        var helpChainLink = new CommandRecognizerChain(
            new HelpCommand(),"-help", jsonChainLink);
        var deleteChainLink = new CommandRecognizerChain(
            new DeleteCommand(_repository), "-delete", helpChainLink);
        var addChainLink = new CommandRecognizerChain(
            new AddCommand(_repository),"-add", deleteChainLink);
        var updateChainLink = new CommandRecognizerChain(
            new UpdateCommand(_repository), "-update", addChainLink);
        var getAllChainLink = new CommandRecognizerChain(
            new GetAllCommand(_repository), "-getAll", updateChainLink);
        _recognizer = new CommandRecognizerChain(
            new GetCommand(_repository), "-get", getAllChainLink);
        
        _parser = new Parser(_recognizer);
    }
}