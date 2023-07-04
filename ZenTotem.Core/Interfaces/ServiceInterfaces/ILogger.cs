namespace ZenTotem.Infrastructure;

/// <summary>
/// Allows you to log everything that happens in the program.
/// </summary>
public interface ILogger
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogError(Exception ex, string message);
}