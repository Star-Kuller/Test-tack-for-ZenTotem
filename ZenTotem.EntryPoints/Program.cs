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

var diContainer = DiContainer.GetContainer();
diContainer.Configure();

IParser parser = diContainer.GetParser();
parser.Parse(args);