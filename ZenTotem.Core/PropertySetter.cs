using ZenTotem.Core.Entities;

namespace ZenTotem.Core.Parser;

public static class PropertySetter
{
    public static Employee SetProperties(string argument, Employee employee)
    {
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