using ZenTotem.Core.Entities;

namespace ZenTotem.Core.Parser;

public static class PropertySetter
{
    public static Employee SetProperties(string argument, Employee employee)
    {
        switch (argument.Split(':')[0])
        {
            case "id":
                break;
            case "FirstName":
                employee.FirstName = argument.Replace("FirstName:", "");
                break;
            case "LastName":
                employee.LastName = argument.Replace("LastName:", "");
                break;
            case "Salary":
                var salary = argument.Replace("Salary:", "");
                if (!decimal.TryParse(salary, out var d))
                    throw new Exception("Error: Wrong format");
                employee.Salary = d;
                break;
            default:
                throw new Exception("Error: Unknown property");
        }

        return employee;
    }
}