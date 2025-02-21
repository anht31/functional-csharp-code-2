using static System.Console;
namespace DesignPatterns.RelatePatterns.ChainOfResponsibilityCommandsHandlers;

interface IHandler
{
    IHandler SetNext(IHandler handler);
    void Handle(object request);
}

abstract class BaseHandler : IHandler
{
    private protected Receiver _receiver;
    IHandler? _next;
    public IHandler SetNext(IHandler handler) => _next = handler;
    public virtual void Handle(object request)
    {
        if (_next != null)
            _next?.Handle(request);
        else
            WriteLine($"No handler for request {request}");
    }
    public BaseHandler(Receiver receiver) => _receiver = receiver;
}

class ProcessOrderCommand : BaseHandler
{
    public override void Handle(object request)
    {
        if (CanHandle(request))
        {
            WriteLine("ProcessOrderCommand do something");
            _receiver.ProcessOrder(request);
        }
        else
            base.Handle(request);
    }
    public ProcessOrderCommand(Receiver receiver) : base(receiver) { }
    private bool CanHandle(object request) => request.ToString() == "a";
}

class CancelOrderCommand : BaseHandler
{
    public override void Handle(object request)
    {
        if (CanHandle(request))
        {
            WriteLine("CancelOrderCommand do something");
            _receiver.CancelOrder(request);
        }
        else
            base.Handle(request);
    }
    public CancelOrderCommand(Receiver receiver) : base(receiver) { }
    private bool CanHandle(object request) => request.ToString() == "b";
}
class ConfirmOrderCommand : BaseHandler
{
    public override void Handle(object request)
    {
        if (CanHandle(request))
        {
            WriteLine("ConfirmOrderCommand do something");
            _receiver.ConfirmOrder(request);
        }
        else
            base.Handle(request);
    }
    public ConfirmOrderCommand(Receiver receiver) : base(receiver) { }
    private bool CanHandle(object request) => request.ToString() == "c";
}

class Receiver
{
    public void ProcessOrder(object param) => WriteLine($"Processing order {param}");
    public void CancelOrder(object param) => WriteLine($"Cancelling order {param}");
    public void ConfirmOrder(object param) => WriteLine($"Confirm order {param}");
}

class Client
{
    public void Run()
    {
        var receiver = new Receiver();
        var a = new ProcessOrderCommand(receiver);
        var b = new CancelOrderCommand(receiver);
        var c = new ConfirmOrderCommand(receiver);
        a.SetNext(b).SetNext(c);

        var request = "c";
        a.Handle(request);
    }
}