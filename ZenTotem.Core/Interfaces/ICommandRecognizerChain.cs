namespace ZenTotem.Core;

public interface ICommandRecognizerChain
{
    public ICommand ReturnCommand(string commandName);
    public ICommandRecognizerChain SetNextChainLink(ICommandRecognizerChain chainLink);
}