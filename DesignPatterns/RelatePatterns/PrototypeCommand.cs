using static System.Console;
namespace DesignPatterns.RelatePatterns.PrototypeCommand;

interface IPrototype
{
    ICommand Clone();
}
interface ICommand : IPrototype
{
    void Execute();
}
class ConcreteCommand : ICommand
{
    Receiver _receiver;
    object _params;
    public ConcreteCommand(Receiver receiver, object param)
    {
        _receiver = receiver;
        _params = param;
    }
    public void Execute() => _receiver.Operation(_params);
    private ConcreteCommand(ConcreteCommand command)
    {
        this._receiver = command._receiver;
        this._params = command._params;
    }
    public ICommand Clone() => new ConcreteCommand(this);
}

class Receiver
{
    private string state = string.Empty;
    public void Operation(object param) => WriteLine(state = $"Receiver do something with {param}");
}
class Invoker
{
    CommandHistory commandHistory = new CommandHistory();
    ICommand _command;
    public void SetCommand(ICommand command) => _command = command;
    public void ExcuteCommand()
    {
        var clone = _command.Clone();
        commandHistory.Backup(clone);
        _command.Execute();
    }
    public void ResetLastestCommnand() => _command = commandHistory.Restore();
}
class CommandHistory
{
    Stack<ICommand> _history = new Stack<ICommand>();
    public void Backup(ICommand command) => _history.Push(command);
    public ICommand Restore() => _history.Pop();
}

class Client
{
    public void Run()
    {
        var invoker = new Invoker();
        var target = new Receiver();
        var commandA = new ConcreteCommand(target, "a");
        var commandB = new ConcreteCommand(target, "b");

        invoker.SetCommand(commandA);
        invoker.ExcuteCommand();
        invoker.SetCommand(commandB);
        invoker.ExcuteCommand();

        invoker.ResetLastestCommnand();
        invoker.ResetLastestCommnand();
        invoker.ExcuteCommand();
    }
}

