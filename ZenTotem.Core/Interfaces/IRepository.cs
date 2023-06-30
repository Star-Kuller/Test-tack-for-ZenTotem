using ZenTotem.Core.Entities;

namespace ZenTotem.Infrastructure;

public interface IRepository
{
    public Task AsyncAdd(Employee employee);
    public Task AsyncDelete(Employee employee);
    public Task<Employee> AsyncGet(int id);
    public Task<List<Employee>> AsyncGetAll();
}