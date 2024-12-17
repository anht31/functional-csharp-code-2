using static System.Console;
namespace DesignPatterns.Study.Command;

public interface ICommand
{
    public void Execute();
}
public class SimpleCommand : ICommand
{
    private string _payload = string.Empty;
    public SimpleCommand(string payload) => _payload = payload;
    public void Execute() => WriteLine($"SimpleCommand: do simple printing: {_payload}");
}
public class ComplexCommand : ICommand
{
    private Receiver _receiver;
    private string _a, _b;
    public ComplexCommand(Receiver receiver, string a, string b)
    {
        _receiver = receiver;
        _a = a;
        _b = b;
    }
    public void Execute()
    {
        WriteLine("ComplexCommand: Complex stuff");
        _receiver.DoSomething(_a);
        _receiver.DoSomethingElse(_b);
    }
}

public class Receiver
{
    public void DoSomething(string a) => WriteLine($"Receiver: Working on {a}.");
    public void DoSomethingElse(string b) => WriteLine($"Receiver: Also working on {b}.");
}

public class Invoker
{
    private ICommand _onStart;
    private ICommand _onFinish;
    public void SetOnStart(ICommand command)
    {
        _onStart = command;
    }
    public void SetOnFinish(ICommand command)
    {
        _onFinish = command;
    }
    public void DoSomethingImportant()
    {
        WriteLine("Invoker: Does anybody want something done before I begin?");
        if (_onStart is ICommand)
        {
            _onStart.Execute();
        }
        WriteLine("Invoker: ...doing something really important...");
        WriteLine("Invoker: Does anybody want something done after I finish?");
        if (_onFinish is ICommand)
        {
            _onFinish.Execute();
        }
    }
}
public class App
{
    public void Run()
    {
        Invoker invoker = new Invoker();
        invoker.SetOnStart(new SimpleCommand("Say Hi!"));
        invoker.SetOnFinish(new ComplexCommand(new Receiver(), "Send email", "Save report"));
        invoker.DoSomethingImportant();
    }
}
