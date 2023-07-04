namespace ZenTotem.Infrastructure;

/// <summary>
/// Receives an error and takes the necessary action.
/// Here it is possible to make a self-diagnosis with subsequent correction.
/// </summary>
public interface IErrorHandler
{
    /// <summary>
    /// Takes action according to the error.
    /// </summary>
    /// <param name="ex">The error to be handled.</param>
    /// <param name="message">Error message.</param>
    void HandleError(Exception ex, string message);
}