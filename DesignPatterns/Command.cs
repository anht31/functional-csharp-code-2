using static System.Console;
namespace Command;

public interface ICommand {
    public void Execute();
}
public class SimpleCommand : ICommand {
    private string _payload = string.Empty;
    public SimpleCommand(string payload) => this._payload = payload;
    public void Execute() => WriteLine($"SimpleCommand: do simple printing: {this._payload}");
}
public class ComplexCommand : ICommand {
    private Receiver _receiver;
    private string _a, _b;
    public ComplexCommand(Receiver receiver, string a, string b) {
        this._receiver = receiver;
        this._a = a;
        this._b = b;
    }
    public void Execute() {
        WriteLine("ComplexCommand: Complex stuff");
        this._receiver.DoSomething(this._a);
        this._receiver.DoSomethingElse(this._b);
    }
}

public class Receiver {
    public void DoSomething(string a) => WriteLine($"Receiver: Working on {a}.");
    public void DoSomethingElse(string b) => WriteLine($"Receiver: Also working on {b}.");
}

public class Invoker
{
    private ICommand _onStart;
    private ICommand _onFinish;
    public void SetOnStart(ICommand command)
    {
        this._onStart = command;
    }
    public void SetOnFinish(ICommand command)
    {
        this._onFinish = command;
    }
    public void DoSomethingImportant()
    {
        WriteLine("Invoker: Does anybody want something done before I begin?");
        if (this._onStart is ICommand)
        {
            this._onStart.Execute();
        }
        WriteLine("Invoker: ...doing something really important...");
        WriteLine("Invoker: Does anybody want something done after I finish?");
        if (this._onFinish is ICommand)
        {
            this._onFinish.Execute();
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
