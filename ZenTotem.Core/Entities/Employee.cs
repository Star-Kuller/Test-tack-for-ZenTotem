using System.Diagnostics.CodeAnalysis;

namespace ZenTotem.Core.Entities;

public class Employee
{
    [NotNull]
    public int Id { get; set; }
    [NotNull]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }
}