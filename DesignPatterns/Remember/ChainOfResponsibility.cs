using static System.Console;
namespace DesignPatterns.Remember.ChainOfResponsibility;

interface IHandler
{
    IHandler SetNext(IHandler handler);
    void Handle(object request);
}

abstract class BaseHandler : IHandler
{
    IHandler? _next;
    public IHandler SetNext(IHandler handler) => _next = handler;
    public virtual void Handle(object request)
    {
        if (_next != null)
            _next?.Handle(request);
        else
            WriteLine($"No handler for request {request}");
    }
}

class ConcreteHandlerA() : BaseHandler
{
    public override void Handle(object request)
    {
        if (CanHandle(request))
            WriteLine("ConcreteHandlerA do something");
        else
            base.Handle(request);
    }

    private bool CanHandle(object request) => request.ToString() == "a";
}

class ConcreteHandlerB() : BaseHandler
{
    public override void Handle(object request)
    {
        if (CanHandle(request))
            WriteLine("ConcreteHandlerB do something");
        else
            base.Handle(request);
    }

    private bool CanHandle(object request) => request.ToString() == "b";
}
class ConcreteHandlerC() : BaseHandler
{
    public override void Handle(object request)
    {
        if (CanHandle(request))
            WriteLine("ConcreteHandlerC do something");
        else
            base.Handle(request);
    }

    private bool CanHandle(object request) => request.ToString() == "c";
}


class Client
{
    public void Run()
    {
        var a = new ConcreteHandlerA();
        var b = new ConcreteHandlerB();
        var c = new ConcreteHandlerC();
        a.SetNext(b).SetNext(c);

        var request = "c";
        a.Handle(request);
    }
}