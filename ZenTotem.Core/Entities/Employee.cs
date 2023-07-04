using System.Diagnostics.CodeAnalysis;

namespace ZenTotem.Core.Entities;

/// <summary>
/// This is the class that represents an employee.
/// </summary>
public class Employee
{
    public Employee(int id)
    {
        if (id < 0)
            throw new Exception("Error: Id cannot be less than 0");
        Id = id;
        Salary = 0;
    }

    [NotNull]
    public int Id { get; }
    public string FirstName { get; set;}
    public string LastName { get; set; }

    public decimal Salary
    {
        get => _salary;
        set => _salary = value >= 0 ? value : 0;

    }

    private decimal _salary;
}