namespace ZenTotem.Core;

public interface ICommandRecognizerChain
{
    
    public ICommand ReturnCommand(string commandName);
}