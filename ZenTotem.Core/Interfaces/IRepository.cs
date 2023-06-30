using ZenTotem.Core.Entities;

namespace ZenTotem.Infrastructure;

public interface IRepository
{
    public void Add(Employee employee);
    public void Delete(int id);
    public Employee Get(int id);
    public List<Employee> GetAll();
}