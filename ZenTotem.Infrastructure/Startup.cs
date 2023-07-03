using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZenTotem.Core;
using ZenTotem.Core.Parser;

namespace ZenTotem.Infrastructure;
public class Startup
{
    private static Startup? _startup;

    // Singleton.
    private Startup(){}
    public static Startup GetStartup()
    {
        if (_startup == null)
            _startup = new Startup();
        return _startup;
    }

    public ServiceProvider Configure(IConfigurationRoot configuration)
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
            .AddTransient<IOutputFormatter, TableGenerator>()
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
        
        service.GetService<JsonRepository>().JsonPath = configuration["jsonFilePath"];
        
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