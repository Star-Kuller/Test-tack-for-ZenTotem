using System.Dynamic;
using ZenTotem.Core.Entities;

namespace ZenTotem.Infrastructure;

/// <summary>
/// Allows you to give the results of the work a look pleasing to the final recipient.
/// </summary>
public interface IOutputFormatter
{
    /// <summary>
    /// Creates a table with fields and properties specified in the header and strings are list instances.
    /// </summary>
    /// <param name="list">List of objects to translate into a table.</param>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <returns>The string representation of the table.</returns>
    public string CreateForList<T>(List<T> list);
    
    /// <summary>
    /// Creates a table with fields and properties in the first column and their values in the second.
    /// </summary>
    /// <param name="tObject">The object instance for which the table will be generated.</param>
    /// <typeparam name="T">Object type of generated table.</typeparam>
    /// <returns>The string representation of the table.</returns>
    public string CreateForOneObject<T>(T obj);
}