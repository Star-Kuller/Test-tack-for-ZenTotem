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

    public async Task AsyncAdd(Employee employee)
    {
        var employees = await Deserialize() ?? new List<Employee>();
        employees.Add(employee);
        await Serialize(employees);
    }

    public async Task AsyncDelete(Employee employee)
    {
        var employees = await Deserialize() ?? throw new Exception("Error: File is empty");
        employees.Remove(employee);
        await Serialize(employees);
    }

    public async Task<Employee> AsyncGet(int id)
    {
        var employees = await Deserialize() ?? throw new Exception("Error: File is empty");
        var employee = from e in employees
            where e.Id == id
            select e;
        if (employees.Count < 1)
            throw new Exception("Error: Employee not found");
        return employee.First();
    }

    public async Task<List<Employee>> AsyncGetAll()
    {
        return await Deserialize() ?? throw new Exception("Error: File is empty");
    }

    
    
    private async Task<List<Employee>?> Deserialize()
    {
        using (FileStream fs = new FileStream(_jsonPath, FileMode.OpenOrCreate))
        {
            return await JsonSerializer.DeserializeAsync<List<Employee>>(fs);
        }
    }
    
    private async Task Serialize(List<Employee> employees)
    {
        using (FileStream fs = new FileStream(_jsonPath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, employees);
        }
    }
}