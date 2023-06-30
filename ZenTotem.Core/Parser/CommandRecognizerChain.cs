namespace ZenTotem.Core.Parser;

public class CommandRecognizerChain : AbstractCommandRecognizerChain
{
    private readonly ICommand _returnCommand;
    private readonly string _commandName;

    public CommandRecognizerChain(ICommand returnCommand, string commandName, ICommandRecognizerChain? nextChainLink) : base(nextChainLink)
    {
        _returnCommand = returnCommand;
        _commandName = commandName.ToLower();
    }

    public override ICommand ReturnCommand(string commandName)
    {
        if (commandName == _commandName)
            return _returnCommand;
        return base.ReturnCommand(commandName);
    }
}
