namespace ZenTotem.Core.Parser;

public class AbstractCommandRecognizerChain : ICommandRecognizerChain
{
    private ICommandRecognizerChain? _nextChainLink;

    public ICommandRecognizerChain SetNextChainLink(ICommandRecognizerChain chainLink)
    {
        _nextChainLink = chainLink;
        return _nextChainLink;
    }
    
    public virtual ICommand ReturnCommand(string inputString)
    {
        if (_nextChainLink is null)
            throw new Exception("Error: Command not recognized");
        return _nextChainLink.ReturnCommand(inputString);
    }
}