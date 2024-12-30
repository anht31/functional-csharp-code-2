using static System.Console;
namespace DesignPatterns.Remember.Command;

interface ICommand
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

        var invoker = new Invoker();
        invoker.SetCommand(new ConcreteCommand(target, "a"));
        invoker.ExcuteCommand();
    }
}
