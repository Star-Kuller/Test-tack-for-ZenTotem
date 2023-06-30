using System.Diagnostics.CodeAnalysis;

namespace ZenTotem.Core.Entities;

public class Employee
{
    public Employee(int id)
    {
        if (id < 0)
            throw new Exception("Error: Id cannot be less than 0");
        Id = id;
    }

    [NotNull]
    public int Id { get; }
    public string FirstName { get; set;}
    public string LastName { get; set; }

    public decimal Salary
    {
        get => Salary;
        set => Salary = value >= 0 ? value : 0;
    }
}