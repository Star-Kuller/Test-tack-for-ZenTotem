namespace ZenTotem.Core.Parser;

public class AbstractCommandRecognizerChain : ICommandRecognizerChain
{
    private ICommandRecognizerChain? _nextChainLink;

    public AbstractCommandRecognizerChain(ICommandRecognizerChain? nextChainLink)
    {
        _nextChainLink = nextChainLink;
    }

    public virtual ICommand ReturnCommand(string inputString)
    {
        if (_nextChainLink is null)
            throw new Exception("Error: Command not recognized");
        return _nextChainLink.ReturnCommand(inputString);
    }
}