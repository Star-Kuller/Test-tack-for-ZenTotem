using System.Text.RegularExpressions;
using ZenTotem.Core.Entities;
using System.Text.Json;

namespace ZenTotem.Infrastructure;

public class JsonRepository : IRepository
{
    private const string PathValidation = @"^(?:[\w]\:|\\)(\\[\w.]+)+$";
    private string _jsonPath;
    public string JsonPath
    {
        set
        {
            if (Regex.IsMatch(value, PathValidation))
            {
                _jsonPath = value;
            }
            else
            {
                throw new Exception("Error: Invalid path");
            }
        }
    }

    public JsonRepository(string jsonPath)
    {
        _jsonPath = jsonPath;
    }

    public async Task AsyncAdd(Employee employee)
    {
        var employees = await Deserialize();
        employees.Add(employee);
        Serialize(employees);
    }

    public async Task AsyncDelete(Employee employee)
    {
        var employees = await Deserialize();
        employees.Remove(employee);
        Serialize(employees);
    }

    public async Task<Employee> AsyncGet(int id)
    {
        var employees = await Deserialize();
        var employee = from e in employees
            where e.Id == id
            select e;
        if (employees.Count < 1)
            throw new Exception("Error: Employee not found");
        return employee.First();
    }

    public async Task<List<Employee>> AsyncGetAll()
    {
        return await Deserialize();
    }

    
    
    private async Task<List<Employee>> Deserialize()
    {
        using (FileStream fs = new FileStream(_jsonPath, FileMode.OpenOrCreate))
        {
            return await JsonSerializer.DeserializeAsync<List<Employee>>(fs) 
                   ?? throw new Exception("Error: file does not match json file");
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