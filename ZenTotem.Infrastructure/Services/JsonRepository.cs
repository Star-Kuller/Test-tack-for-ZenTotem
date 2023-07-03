using ZenTotem.Core.Entities;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ZenTotem.Infrastructure;

public class JsonRepository : IRepository
{
    public string JsonPath;
    private readonly ILogger? _logger;

    public JsonRepository(ILogger? logger = null)
    {
        _logger = logger;
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        JsonPath = configuration["jsonFilePath"];
    }
    
    public void Add(Employee employee)
    {
        var employees = Deserialize() ?? new List<Employee>();
        employees.Add(employee);
        Serialize(employees);
        if (_logger is not null)
            _logger.LogInfo($"Added employee: {employee.Id}");
    }

    public void Update(Employee employee)
    {
        var employees = Deserialize() ?? new List<Employee>();
        var oldEmployee = employees.FirstOrDefault(e => e.Id == employee.Id)
                          ?? throw new Exception("Error: Employee not found");;
        employees.Remove(oldEmployee);
        employees.Add(employee);
        Serialize(employees);
        if (_logger is not null)
            _logger.LogInfo($"Updated employee: {employee.Id}");
    }

    public void Delete(int id)
    {
        var employees = Deserialize()
                        ?? throw new Exception("Error: File is empty");
        var employee = employees.FirstOrDefault(e => e.Id == id)
                       ?? throw new Exception("Error: Employee not found");
        employees.Remove(employee);
        Serialize(employees);
        if (_logger is not null)
            _logger.LogInfo($"Removed employee: {employee.Id}");
    }

    public Employee Get(int id)
    {
        var employees = Deserialize()
                        ?? throw new Exception("Error: File is empty");
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
        using (FileStream fs = new FileStream(JsonPath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<List<Employee>>(fs);
        }
    }
    
    private void Serialize(List<Employee> employees)
    {
        using (FileStream fs = new FileStream(JsonPath, FileMode.Truncate))
        {
            JsonSerializer.Serialize(fs, employees);
        }
    }
}