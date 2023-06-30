using ZenTotem.Core.Entities;
using System.Text.Json;

namespace ZenTotem.Infrastructure;

public class JsonRepository : IRepository
{
    private readonly string _jsonPath;

    public JsonRepository(string jsonPath)
    {
        _jsonPath = jsonPath;
    }

    public void Add(Employee employee)
    {
        var employees = Deserialize() ?? new List<Employee>();
        employees.Add(employee);
        Serialize(employees);
    }

    public void Delete(int id)
    {
        var employees = Deserialize() ?? throw new Exception("Error: File is empty");
        var employeeCollection = from e in employees
            where e.Id == id
            select e;
        var employee = employeeCollection.FirstOrDefault() ?? throw new Exception("Error: Employee not found");
        employees.Remove(employee);
        Serialize(employees);
    }

    public Employee Get(int id)
    {
        var employees = Deserialize() ?? throw new Exception("Error: File is empty");
        var employee = from e in employees
            where e.Id == id
            select e;
        if (employees.Count < 1)
            throw new Exception("Error: Employee not found");
        return employee.First();
    }

    public List<Employee> GetAll()
    {
        return Deserialize() ?? throw new Exception("Error: File is empty");
    }

    
    
    private List<Employee>? Deserialize()
    {
        using (FileStream fs = new FileStream(_jsonPath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<List<Employee>>(fs);
        }
    }
    
    private void Serialize(List<Employee> employees)
    {
        using (FileStream fs = new FileStream(_jsonPath, FileMode.Truncate))
        {
            JsonSerializer.Serialize(fs, employees);
        }
    }
}