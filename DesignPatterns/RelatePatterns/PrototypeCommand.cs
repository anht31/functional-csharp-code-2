using static System.Console;
namespace DesignPatterns.RelatePatterns.PrototypeCommand;

interface IPrototype
{
    ICommand Clone();
}
interface ICommand
{
    void Execute();
}
class ConcreteCommand : ICommand, IPrototype
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
    public void Operation(object param) => WriteLine($"Receiver do something with {param}");
}
class Invoker
{
    ICommand _command;
    public void SetCommand(ICommand command) => _command = command;
    public void ExcuteCommand() => _command.Execute();
}

class Client
{
    public void Run()
    {
        var target = new Receiver();
        var command = new ConcreteCommand(target, "a");

        var invoker = new Invoker();
        invoker.SetCommand(command);
        invoker.ExcuteCommand();

        var invoker2 = new Invoker();
        invoker2.SetCommand(command.Clone());
        invoker2.ExcuteCommand();
    }
}

