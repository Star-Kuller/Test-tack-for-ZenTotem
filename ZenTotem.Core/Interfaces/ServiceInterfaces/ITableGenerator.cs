using System.Dynamic;
using ZenTotem.Core.Entities;

namespace ZenTotem.Infrastructure;

public interface ITableGenerator
{
    public string Create<T>(List<T> list);
    public string CreateForOneRow<T>(T employee);
}