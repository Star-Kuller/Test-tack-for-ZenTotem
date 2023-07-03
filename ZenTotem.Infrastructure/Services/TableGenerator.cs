using System.Reflection;
using System.Text;

namespace ZenTotem.Infrastructure;
public class TableGenerator : IOutputFormatter
{
    
    public const int ColumnWidthForOneObject = 14;
    public const int ColumnWidth = 15;
    public const int SeparatorStick = 1;
    public const int Space = 1;

    public string CreateForList<T>(List<T> list)
    {
        var sb = new StringBuilder(200);
        var lineWidth = GetWidthForList<T>();
        sb.AppendLine(new string('-', lineWidth));
        sb.AppendLine(CreateHeaders<T>());
        
        // Headers separator.
        var sepChars = new char[lineWidth - 2]; // leave two char for the column separator.
        Array.Fill(sepChars, '-');
        sb.AppendLine($"|{new string(sepChars)}|");
        
        // Create rows.
        foreach (var obj in list)
        {
            sb.AppendLine(AddPropertyToColumns(obj));
            sb.AppendLine(AddFieldsToColumns(obj));
            sb.AppendLine("|");
        }

        sb.AppendLine(new string('-', lineWidth));
        return sb.ToString();
    }
    
    public string CreateForOneObject<T>(T tObject)
    {
        var sb = new StringBuilder(200);
        sb.AppendLine(GetLineForOne());

        AddPropertyLines(tObject, sb);
        AddFieldLines(tObject, sb);

        sb.AppendLine(GetLineForOne());
        return sb.ToString();
    }

    // For List.
    
    private int GetWidthForList<T>()
    {
        var lineWidth = typeof(T).GetProperties().Length * 
                        (ColumnWidth + Space + SeparatorStick);
        lineWidth += typeof(T).GetFields().Length * 
                     (ColumnWidth + Space + SeparatorStick);
        lineWidth += SeparatorStick;
        return lineWidth;
    }

    private string CreateHeaders<T>()
    {
        var header = new StringBuilder();
        foreach (var field in typeof(T).GetProperties().Concat<MemberInfo>(typeof(T).GetFields()))
        {
            var fieldName = field.Name.Length > ColumnWidth
                ? $"{field.Name.Substring(0, ColumnWidth - 3)}..."
                : field.Name;
            header.Append($"| {fieldName, -ColumnWidth}");
        }
        header.Append("|");
        return header.ToString();
    }

    private string AddPropertyToColumns<T>(T obj)
    {
        var sb = new StringBuilder();
        foreach (var member in typeof(T).GetProperties())
        {
            var value = "";
            if((member.GetValue(obj)?.ToString() ?? string.Empty).Length > ColumnWidth)
            {
                value = (member.GetValue(obj)?.ToString()
                            ?? string.Empty)
                    .Substring(0, ColumnWidth - 3); // leave 3 for dot.
                value += "...";
            }
            else
            {
                value = member.GetValue(obj)?.ToString() 
                        ?? string.Empty;
            }

            sb.Append($"| {value,-ColumnWidth}");
        }

        return sb.ToString();
    }
    
    private string AddFieldsToColumns<T>(T obj)
    {
        var sb = new StringBuilder();
        foreach (var member in typeof(T).GetFields())
        {
            var value = "";
            if((member.GetValue(obj)?.ToString() ?? string.Empty).Length > ColumnWidth)
            {
                value = (member.GetValue(obj)?.ToString()
                         ?? string.Empty)
                    .Substring(0, ColumnWidth - 3); // leave 3 for dot.
                value += "...";
            }
            else
            {
                value = member.GetValue(obj)?.ToString() 
                        ?? string.Empty;
            }

            sb.Append($"| {value,-ColumnWidth}");
        }

        return sb.ToString();
    }
    
    // For one object.
    
    private string GetLineForOne()
    {
        return new string('-', (ColumnWidthForOneObject + Space * 2) * 2 + SeparatorStick * 3);
    }

    private void AddPropertyLines<T>(T tObject, StringBuilder sb)
    {
        foreach (var member in typeof(T).GetProperties())
        {
            AddLineForMemberForOne(member.Name, member.GetValue(tObject)?.ToString() ?? string.Empty, sb);
        }
    }

    private void AddFieldLines<T>(T tObject, StringBuilder sb)
    {
        foreach (var member in typeof(T).GetFields())
        {
            AddLineForMemberForOne(member.Name, member.GetValue(tObject)?.ToString() ?? string.Empty, sb);
        }
    }

    private void AddLineForMemberForOne(string name, string value, StringBuilder sb)
    {
        var formattedValue = 
            value.Length > ColumnWidthForOneObject 
                ? value.Substring(0, ColumnWidthForOneObject - 3) + "..." 
                : value;

        sb.AppendFormat("| {0,-" + ColumnWidthForOneObject + "} | {1,-" + ColumnWidthForOneObject + "} |\n", 
            name.Length > ColumnWidthForOneObject 
                ? name.Substring(0, ColumnWidthForOneObject - 3) + "..."
                : name, formattedValue);
    }
}