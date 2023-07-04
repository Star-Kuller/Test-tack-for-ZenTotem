using Microsoft.Extensions.DependencyInjection;
using ZenTotem.Core;
using ZenTotem.Core.Parser;

namespace ZenTotem.Infrastructure;

/// <summary>
/// This class defines the parameters and necessary relationships in the application.
/// </summary>
public class Startup
{
    private static Startup? _startup;

    /// <summary>
    /// Lets get a singleton instance.
    /// </summary>
    private Startup(){}
    public static Startup GetStartup()
    {
        return _startup ?? (_startup = new Startup());
    }

    /// <summary>
    /// Creates the "Appsettings.json" file if it has been removed.
    /// Creates a DI container.
    /// Creates a dictionary of command and key mappings.
    /// </summary>
    /// <returns>
    /// An object of type ServiceProvider.
    /// Needed to get the classes in the DI container.
    /// </returns>
    public ServiceProvider Configure()
    {
        if (!File.Exists("appsettings.json"))
        {
            File.Create("appsettings.json")
                .Close();
            using (StreamWriter writer = new StreamWriter("appsettings.json", false))
            {
                writer.WriteLineAsync("{\"jsonFilePath\": \"/Employees.json\"}");
            }
        }

        var service = new ServiceCollection()
            .AddSingleton<IRepository, JsonRepository>()
            .AddSingleton<IParser, Parser>()
            //.AddTransient<IOutputFormatter, TableGenerator>()
            .AddTransient<IOutputFormatter, OutputFormatter>()
            .AddTransient<IErrorHandler, ErrorHandler>()
            .AddTransient<IOutput, OutputToConsole>()
            .AddTransient<HelpCommand>()
            .AddTransient<JsonCommand>()
            .AddTransient<AddCommand>()
            .AddTransient<DeleteCommand>()
            .AddTransient<GetCommand>()
            .AddTransient<GetAllCommand>()
            .AddTransient<UpdateCommand>()
            .BuildServiceProvider();
        
        var dictionaryCommands = new Dictionary<string, ICommand>
        {
            {"-help", service.GetService<HelpCommand>()},
            {"-json", service.GetService<JsonCommand>()},
            {"-add", service.GetService<AddCommand>()},
            {"-delete", service.GetService<DeleteCommand>()},
            {"-get", service.GetService<GetCommand>()},
            {"-getall", service.GetService<GetAllCommand>()},
            {"-update", service.GetService<UpdateCommand>()}
        };

        service.GetService<IParser>().DictionaryCommands = dictionaryCommands;
        
        return service;
    }
}