using ZenTotem.Core.Entities;

namespace ZenTotem.Infrastructure;

/// <summary>
/// Allows you to implement the repository pattern and replace the implementation to work with any data store.
/// </summary>
public interface IRepository
{
    /// <summary>
    /// Adds a new employee.
    /// </summary>
    public void Add(Employee employee);
    
    /// <summary>
    /// Updates an employee.
    /// </summary>
    public void Update(Employee employee);
    
    /// <summary>
    /// Removes an employee from.
    /// </summary>
    public void Delete(int id);
    
    /// <summary>
    /// Gets the employee with the specified ID.
    /// </summary>
    public Employee Get(int id);
    
    /// <summary>
    /// Gets a list of all employees.
    /// </summary>
    public List<Employee> GetAll();
}