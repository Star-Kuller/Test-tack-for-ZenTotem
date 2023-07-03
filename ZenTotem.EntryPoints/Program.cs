using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZenTotem.Core;
using ZenTotem.Infrastructure;

try
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");
    var configuration = builder.Build();

    var startup = Startup.GetStartup();
    var serviceProvider = startup.Configure(configuration);

    var parser = serviceProvider.GetService<IParser>();
    var command = parser.Parse(args);
    var commandArguments = args.ToList();
    commandArguments.RemoveAt(0);
    command.Execute(commandArguments);
}
catch (Exception e)
{
    Console.WriteLine(e);
}