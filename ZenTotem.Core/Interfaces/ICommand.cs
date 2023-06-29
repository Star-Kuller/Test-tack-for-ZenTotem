namespace ZenTotem.Core;

public interface ICommand
{
    public void Execute(List<string> arguments);
}