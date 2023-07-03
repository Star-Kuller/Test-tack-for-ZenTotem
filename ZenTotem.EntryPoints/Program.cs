using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZenTotem.Core;
using ZenTotem.Infrastructure;


var startup = Startup.GetStartup();
var serviceProvider = startup.Configure();

try
{
    var parser = serviceProvider.GetService<IParser>();
    var command = parser.Parse(args);
    var commandArguments = args.ToList();
    commandArguments.RemoveAt(0);
    command.Execute(commandArguments);
}
catch (Exception e)
{
    var errorHandler = serviceProvider.GetService<IErrorHandler>();
    errorHandler.HandleError(e, e.Message);
}