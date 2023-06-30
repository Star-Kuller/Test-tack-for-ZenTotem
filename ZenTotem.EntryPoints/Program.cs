using Microsoft.Extensions.Configuration;
using ZenTotem.Core;
using ZenTotem.Infrastructure;

if (!File.Exists("appsettings.json"))
{
    File.Create("appsettings.json")
        .Close();
    using (StreamWriter writer = new StreamWriter("appsettings.json", false))
    {
        writer.WriteLineAsync("{\"jsonFilePath\": \"/Employees.json\"}");
    }
}

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");
var configuration = builder.Build();

var diContainer = DiContainer.GetContainer();
diContainer.Configure(configuration);

IParser parser = diContainer.GetParser();
parser.Parse(args);