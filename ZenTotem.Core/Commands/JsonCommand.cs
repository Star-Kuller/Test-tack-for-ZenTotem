using System.Net;
using System.Text.Json;

namespace ZenTotem.Core;

public class JsonCommand : ICommand
{
    public void Execute(List<string> arguments)
    {
        if (arguments.Count < 1 || arguments.Count > 2)
            throw new Exception("Error: Wrong number of arguments");
        if (!arguments[0].Contains("path:"))
            throw new Exception("Error: Invalid syntax");
        if (arguments.Count == 2 && !arguments[1].Contains("name:"))
            throw new Exception("Error: Invalid syntax");
        var path = arguments[0].Replace("path:", "");
        
        
        if (arguments.Count == 2 && arguments[1].Contains("name:"))
            path = AddName(path, arguments[1].Replace("name:", ""));
        
        if (path.Split('.')[^1] != "json")
            path += ".json";
        
        if (!File.Exists(path))
        {
            File.Create(path).Close();
            File.WriteAllText(path, "{}");
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