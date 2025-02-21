using static System.Console;
namespace DesignPatterns.RelatePatterns.ChainOfResponsibilityCommandsRequests;

interface ICommand
{
    void Execute(object message);
    void SetReceiver(ILoggerReceiver logger);
}
class LogCommand : ICommand
{
    private ILoggerReceiver? _receiver;
    public void SetReceiver(ILoggerReceiver logger) => _receiver = logger;
    public void Execute(object message) => _receiver?.Log(message.ToString());
}

interface IHandler
{
    IHandler SetNext(IHandler handler);
    void Handle(ICommand command);
}
abstract class BaseHandler : IHandler
{
    IHandler? _next;
    private protected ICommand _command;
    public IHandler SetNext(IHandler handler) => _next = handler;
    public virtual void Handle(ICommand command)
    {
        if (_next != null)
            _next?.Handle(command);
        else
            WriteLine($"No handler for request {command}");
    }
    private protected BaseHandler(ICommand command) => _command = command;
}

class OrderProcessStep : BaseHandler
{
    public OrderProcessStep(ICommand command) : base(command) { }
    public override void Handle(ICommand command)
    {
        if (CanHandle(command))
        {
            WriteLine("OrderProcessStep do something");
        }
        else
            base.Handle(command);
    }
    private bool CanHandle(ICommand command) => request.ToString() == "a";
}

class OrderCancelStep : BaseHandler
{
    public OrderCancelStep(ICommand command) : base(command) { }
    public override void Handle(ICommand command)
    {
        if (CanHandle(request))
        {
            WriteLine("OrderCancelStep do something");
            _command.SetReceiver(new ServerLogger());
            _command.Execute(request);
        }
        else
            base.Handle(request);
    }
    private bool CanHandle(ICommand command) => request.ToString() == "b";
}
class OrderComfirmStep : BaseHandler
{
    public OrderComfirmStep(ICommand command) : base(command) { }
    public override void Handle(ICommand command)
    {
        if (CanHandle(request))
        {
            WriteLine("OrderComfirmStep do something");
            _command.SetReceiver(new DatabaseLogger());
            _command.Execute(request);
        }
        else
            base.Handle(request);
    }
    private bool CanHandle(ICommand command) => request.ToString() == "c";
}

interface ILoggerReceiver
{
    void Log(string message);
}
class FileLogger : ILoggerReceiver
{
    public void Log(string message) => WriteLine($"FileLogger: {message}");
}
class ServerLogger : ILoggerReceiver
{
    public void Log(string message) => WriteLine($"ServerLogger: {message}");
}
class DatabaseLogger : ILoggerReceiver
{
    public void Log(string message) => WriteLine($"DatabaseLogger: {message}");
}

class Client
{
    public void Run()
    {
        var command = new LogCommand();

        var a = new OrderProcessStep();
        var b = new OrderCancelStep();
        var c = new OrderComfirmStep();
        a.SetNext(b).SetNext(c);

        a.Handle(command);
    }
}