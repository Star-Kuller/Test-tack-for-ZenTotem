using ZenTotem.Core.Entities;

namespace ZenTotem.Core.Parser;
/// <summary>
/// A class for writing values to the fields and properties of the employee class by the corresponding key.
/// </summary>
public static class PropertySetter
{
    /// <summary>
    /// Gets an attribute and writes its value to the corresponding field.
    /// </summary>
    /// <param name="argument">Argument in the format {name}:{value}</param>
    /// <param name="employee">The employee being manipulated.</param>
    /// <returns>Employee with entered fields.</returns>
    public static Employee SetProperties(string argument, Employee employee)
    {
        if (employee == null)
            throw new Exception("Error: No employee for set properties");
        switch (argument.Split(':')[0].ToLower())
        {
            case "id":
                break;
            case "firstname":
                employee.FirstName = argument.Replace("FirstName:", "",
                    StringComparison.InvariantCultureIgnoreCase);
                break;
            case "lastname":
                employee.LastName = argument.Replace("LastName:", "",
                    StringComparison.InvariantCultureIgnoreCase);
                break;
            case "salary":
                var salary = argument.Replace("Salary:", "",
                    StringComparison.InvariantCultureIgnoreCase);
                salary = salary.Replace(".", ",");
                if (!decimal.TryParse(salary, out var d))
                    throw new Exception("Error: Wrong decimal format");
                employee.Salary = d;
                break;
            default:
                throw new Exception("Error: Unknown property");
        }

        return employee;
    }
}