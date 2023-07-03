using System.Dynamic;
using ZenTotem.Core.Entities;

namespace ZenTotem.Infrastructure;

public interface IOutputFormatter
{
    public string CreateForList<T>(List<T> list);
    public string CreateForOneObject<T>(T employee);
}