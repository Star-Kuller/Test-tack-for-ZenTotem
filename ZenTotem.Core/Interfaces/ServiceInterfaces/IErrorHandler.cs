namespace ZenTotem.Infrastructure;

public interface IErrorHandler
{
    string HandleError(Exception ex, string message);
}