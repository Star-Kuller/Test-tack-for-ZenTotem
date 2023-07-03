namespace ZenTotem.Infrastructure;

public interface IErrorHandler
{
    void HandleError(Exception ex, string message);
}