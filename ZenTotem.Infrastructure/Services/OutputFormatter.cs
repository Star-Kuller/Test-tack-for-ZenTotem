using System.Reflection;
using System.Text;

namespace ZenTotem.Infrastructure;

/// <summary>
/// Translates any object(s) into a string.
/// </summary>
public class OutputFormatter : IOutputFormatter
{
    public string CreateForList<T>(List<T> list)
    {
        var sb = new StringBuilder(200);
        foreach (var obj in list)
        {
            sb.AppendLine(CreateForOneObject(obj));
        }
        return sb.ToString();
    }

    public string CreateForOneObject<T>(T obj)
    {
        var sb = new StringBuilder(75);
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            sb.Append($"{property.Name} = {property.GetValue(obj)}, ");
        }

        foreach (var field in fields)
        {
            sb.Append($"{field.Name} = {field.GetValue(obj)}, ");
        }

        return sb.ToString().TrimEnd(' ', ',');
    }
}