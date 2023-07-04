using ZenTotem.Core.Entities;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ZenTotem.Infrastructure;
/// <summary>
/// A class that allows you to work with JSON. It is an implementation of the Repository pattern.
/// </summary>
public class JsonRepository : IRepository
{
    public string JsonPath;
    private readonly ILogger? _logger;
    
    /// <param name="logger">Optional. Allows you to enable logging.</param>
    public JsonRepository(ILogger? logger = null)
    {
        _logger = logger;
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        JsonPath = configuration["jsonFilePath"];
    }
    
    /// <summary>
    /// Adds a new employee to the JSON file.
    /// </summary>
    public void Add(Employee employee)
    {
        var employees = Deserialize() ?? new List<Employee>();
        employees.Add(employee);
        Serialize(employees);
        if (_logger is not null)
            _logger.LogInfo($"Added employee: {employee.Id}");
    }
    
    /// <summary>
    /// Updates an employee in a JSON file.
    /// </summary>
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

    /// <summary>
    /// Removes an employee from the JSON file.
    /// </summary>
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

    /// <summary>
    /// Gets the employee with the specified ID.
    /// </summary>
    public Employee Get(int id)
    {
        var employees = Deserialize()
                        ?? throw new Exception("Error: File is empty");
        var employee = from e in employees
            where e.Id == id
            select e;
        if (!employee.Any())
            throw new Exception("Error: Employee not found");
        return employee.First();
    }

    /// <summary>
    /// Gets a list of all employees.
    /// </summary>
    public List<Employee> GetAll()
    {
        return Deserialize() ?? throw new Exception("Error: File is empty");
    }

    
    
    private List<Employee>? Deserialize()
    {
        try
        {
            using (FileStream fs = new FileStream(JsonPath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<List<Employee>>(fs);
            }
        }
        catch (Exception e)
        {
            Serialize(new List<Employee>());
            using (FileStream fs = new FileStream(JsonPath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<List<Employee>>(fs);
            }
            throw;
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