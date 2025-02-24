
using static System.Console;
namespace DesignPatterns.RelatePatterns.ChainOfResponsibilityCommandsRequestsCanHandle;

interface ICommand
{
    void Execute(object message);
    void SetReceiver(ILoggerReceiver logger);
}
class ServerLogCommand : ICommand
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
    public IHandler SetNext(IHandler handler) => _next = handler;
    public virtual void Handle(ICommand command)
    {
        if (_next != null)
            _next?.Handle(command);
        else
            WriteLine($"No handler for request {command}");
    }
}

class FileLogHandler : BaseHandler
{
    public override void Handle(ICommand command)
    {
        var receiver = new FileLogger();
        if (receiver.CanHandle())
        {
            WriteLine("FileLogHandler do something");
            command.SetReceiver(receiver);
            command.Execute("FileLogHandler");
        }
        else
            base.Handle(command);
    }
}

class ServerLogHandler : BaseHandler
{
    public override void Handle(ICommand command)
    {
        var receiver = new ServerLogger();
        if (receiver.CanHandle())
        {
            WriteLine("ServerLogHandler do something");
            command.SetReceiver(receiver);
            command.Execute("ServerLogHandler");
        }
        else
            base.Handle(command);
    }
}
class DatabaseLogHandler : BaseHandler
{
    public override void Handle(ICommand command)
    {
        var receiver = new DatabaseLogger();
        if (receiver.CanHandle())
        {
            WriteLine("DatabaseLogHandler do something");
            command.SetReceiver(receiver);
            command.Execute("DatabaseLogHandler");
        }
        else
            base.Handle(command);
    }
}

interface ILoggerReceiver
{
    bool CanHandle();
    void Log(string message);
}
class FileLogger : ILoggerReceiver
{
    public bool CanHandle() => false;
    public void Log(string message) => WriteLine($"FileLogger: {message}");
}
class ServerLogger : ILoggerReceiver
{
    public bool CanHandle() => true;
    public void Log(string message) => WriteLine($"ServerLogger: {message}");
}
class DatabaseLogger : ILoggerReceiver
{
    public bool CanHandle() => false;
    public void Log(string message) => WriteLine($"DatabaseLogger: {message}");
}

class Client
{
    public void Run()
    {
        var orderCancel = new ServerLogCommand();

        var a = new FileLogHandler();
        var b = new ServerLogHandler();
        var c = new DatabaseLogHandler();
        a.SetNext(b).SetNext(c);

        a.Handle(orderCancel);
    }
}
