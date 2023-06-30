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
    
    public void Configure()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var configuration = builder.Build();

        _repository = new JsonRepository(configuration["jsonFilePath"]);
        
        // Command recognizer chain.
        _recognizer = new CommandRecognizerChain(
            new JsonCommand(),"-json");
        var helpChainLink = new CommandRecognizerChain(
            new HelpCommand(),"-help");
        
        // Set the order of checks. It is recommended to set up in from the most frequent team to the rarest.
        _recognizer.SetNextChainLink(helpChainLink);

        _parser = new Parser(_recognizer);
    }
}