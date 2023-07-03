using System.Text;

namespace ZenTotem.Infrastructure;
public class OutputFormatter : IOutputFormatter
{
    
    // TO DO Отрефакторить этот класс
    private const int ColumnWidthForOneObject = 14;
    private const int ColumnWidth = 15;
    private const int SplitStick = 1;
    private const int Space = 1;

    public string CreateForList<T>(List<T> list)
    {
        var sb = new StringBuilder(200);
        var lineWidth = typeof(T).GetProperties().Length * (ColumnWidth + Space + SplitStick);
        lineWidth += typeof(T).GetFields().Length * (ColumnWidth + Space + SplitStick);
        lineWidth += SplitStick;
        sb.AppendLine(new string('-', lineWidth));

        foreach (var field in typeof(T).GetProperties())
        {
            if (field.Name.Length > ColumnWidth)
            {
                sb.Append($"| {field.Name.Substring(0, ColumnWidth - 3)}...");
                continue;
            }
            sb.Append($"| {field.Name, -ColumnWidth}");
        }
        foreach (var field in typeof(T).GetFields())
        {
            if (field.Name.Length > ColumnWidth)
            {
                sb.Append($"| {field.Name.Substring(0, ColumnWidth - 3)}...");
                continue;
            }
            sb.Append($"| {field.Name, -ColumnWidth}");
        }
        sb.Append("|\n|");
        sb.Append(new string('-', lineWidth-2));
        sb.AppendLine("|");
        foreach (var obj in list)
        {
            foreach (var field in typeof(T).GetProperties())
            {
                if ((field.GetValue(obj)?.ToString()
                     ?? string.Empty).Length > ColumnWidth)
                {
                    sb.Append($"| {(field.GetValue(obj)?.ToString()
                                  ?? string.Empty).Substring(0, ColumnWidth - 3)}...");
                    continue;
                }
                sb.Append($"| {field.GetValue(obj)?.ToString()
                              ?? string.Empty, -ColumnWidth}");
            }
            foreach (var field in typeof(T).GetFields())
            {
                if ((field.GetValue(obj)?.ToString()
                     ?? string.Empty).Length > ColumnWidth)
                {
                    sb.Append($"| {(field.GetValue(obj)?.ToString()
                                  ?? string.Empty).Substring(0, ColumnWidth - 3)}...");
                    continue;
                }
                sb.Append($"| {field.GetValue(obj)?.ToString()
                              ?? string.Empty, -ColumnWidth}");
            }
            sb.AppendLine("|");
        }
        sb.AppendLine(new string('-', lineWidth));
        return sb.ToString();
    }
    
    public string CreateForOneObject<T>(T tObject)
    {
        var sb = new StringBuilder(200);
        sb.AppendLine(new string('-', (ColumnWidthForOneObject + Space * 2) * 2 + SplitStick * 3));

        foreach (var field in typeof(T).GetProperties())
        {
            if (field.Name.Length > ColumnWidthForOneObject)
            {
                sb.Append($"| {field.Name.Substring(0, ColumnWidthForOneObject - 3)}...");
            }
            else
            {
                sb.Append($"| {field.Name,-ColumnWidthForOneObject} ");
            }
            if ((field.GetValue(tObject)?.ToString()
                 ?? string.Empty).Length > ColumnWidthForOneObject)
            {
                sb.AppendLine($"| {(field.GetValue(tObject)?.ToString()
                                ?? string.Empty).Substring(0, ColumnWidthForOneObject - 3)}... |");
            }
            else
            {
                sb.AppendLine($"| {field.GetValue(tObject)?.ToString()
                                   ?? string.Empty,-ColumnWidthForOneObject} |");
            }
        }

        foreach (var field in typeof(T).GetFields())
        {
            if (field.Name.Length > ColumnWidthForOneObject)
            {
                sb.Append($"| {field.Name.Substring(0, ColumnWidthForOneObject - 3)}...");
            }
            else
            {
                sb.AppendLine($"| {field.Name,-ColumnWidthForOneObject} ");
            }
            if ((field.GetValue(tObject)?.ToString()
                 ?? string.Empty).Length > ColumnWidthForOneObject)
            {
                sb.AppendLine($"| {(field.GetValue(tObject)?.ToString()
                                ?? string.Empty).Substring(0, ColumnWidthForOneObject - 3)}... |");
            }
            else
            {
                sb.AppendLine($"| {field.GetValue(tObject)?.ToString()
                                   ?? string.Empty,-ColumnWidthForOneObject} |");
            }
        }
    
        sb.AppendLine(new string('-', (ColumnWidthForOneObject + Space * 2) * 2 + SplitStick * 3));
        return sb.ToString();
    }
}