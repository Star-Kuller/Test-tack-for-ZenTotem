using System.Net;
using System.Text.Json;
using ZenTotem.Core.Entities;

namespace ZenTotem.Core;

public class JsonCommand : ICommand
{
    public void Execute(List<string> arguments)
    {
        if (arguments.Count < 1 || arguments.Count > 2)
            throw new Exception("Error: Wrong number of arguments");
        
        if (!arguments[0].Contains("path:",
                StringComparison.InvariantCultureIgnoreCase))
            throw new Exception("Error: Invalid syntax");
        
        if (arguments.Count == 2 && !arguments[1].Contains("name:",
                StringComparison.InvariantCultureIgnoreCase))
            throw new Exception("Error: Invalid syntax");
        
        var path = arguments[0].Replace("path:", "",
            StringComparison.InvariantCultureIgnoreCase);
        
        
        if (arguments.Count == 2 && arguments[1].Contains("name:",
                StringComparison.InvariantCultureIgnoreCase))
            path = AddName(path, arguments[1].Replace("name:", "",
                StringComparison.InvariantCultureIgnoreCase));
        
        if (path.Split('.')[^1] != "json")
            path += ".json";
        
        if (!File.Exists(path))
        {
            File.Create(path).Close();
            string newJson = JsonSerializer.Serialize(new List<Employee>());
            File.WriteAllText(path, newJson);
        }

        UpdatePathInSettings(path);
        
        Console.WriteLine($"Selected file by path {path}");
    }

    private string AddName(string pathDirectory, string name)
    {
        if (pathDirectory[^1] != '\\' && pathDirectory[^1] != '/')
            pathDirectory += '\\';
        return pathDirectory + name;
    }

    private void UpdatePathInSettings(string path)
    {
        string settingsJson = File.ReadAllText("appsettings.json");
        var settings = JsonSerializer.Deserialize<Dictionary<string, object>>(settingsJson);
        settings["jsonFilePath"] = path;
        string newJsonString = JsonSerializer.Serialize(settings);
        File.WriteAllText("appsettings.json", newJsonString);
    }
}