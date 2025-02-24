using System.Net.Http.Headers;
using static System.Console;
namespace DesignPatterns.RelatePatterns.ChainOfResponsibilityCommandsRequests;

interface ICommand
{
    bool Execute(object message);
    void SetReceiver(ILoggerReceiver logger);
}
class LogCommand : ICommand
{
    private ILoggerReceiver? _receiver;
    public void SetReceiver(ILoggerReceiver logger) => _receiver = logger;
    public bool Execute(object message) => _receiver?.Process(message.ToString()) ?? false;
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
        command.SetReceiver(new FileLogger());
        bool  canHandle = command.Execute("FileLogHandler");
        if (canHandle)
        {
            WriteLine("FileLogHandler do something");
        }
        else
            base.Handle(command);
    }
}

class ServerLogHandler : BaseHandler
{
    public override void Handle(ICommand command)
    {
        command.SetReceiver(new ServerLogger());
        bool canHandler = command.Execute("ServerLogHandler");
        if (canHandler)
        {
            WriteLine("ServerLogHandler do something");
        }
        else
            base.Handle(command);
    }
}
class DatabaseLogHandler : BaseHandler
{
    public override void Handle(ICommand command)
    {
        command.SetReceiver(new DatabaseLogger());
        bool canHandler = command.Execute("DatabaseLogHandler");
        if (canHandler)
        {
            WriteLine("DatabaseLogHandler do something");
        }
        else
            base.Handle(command);
    }
}

interface ILoggerReceiver
{
    bool Process(string message);
}
class FileLogger : ILoggerReceiver
{
    public bool Process(string message)
    {
        WriteLine($"FileLogger: {message}");
        return false;
    }
}
class ServerLogger : ILoggerReceiver
{
    public bool Process(string message)
    {
        WriteLine($"ServerLogger: {message}");
        return true;
    }
}
class DatabaseLogger : ILoggerReceiver
{
    public bool Process(string message)
    {
        WriteLine($"DatabaseLogger: {message}");
        return false;
    }
}

class Client
{
    public void Run()
    {
        var orderCancel = new LogCommand();

        var a = new FileLogHandler();
        var b = new ServerLogHandler();
        var c = new DatabaseLogHandler();
        a.SetNext(b).SetNext(c);

        a.Handle(orderCancel);
    }
}