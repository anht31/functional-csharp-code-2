using System.Runtime.InteropServices.Marshalling;
using static System.Console;
namespace DesignPatterns.RelatePatterns.CompositeChainOfResponsibility;

interface IHandler
{
    IHandler SetNext(IHandler handler);
    void Handle(object request);
}
interface IComponent
{
    void Excute();
}

abstract class Component : IComponent, IHandler
{
    private IHandler? _next;
    public abstract void Excute();

    public IHandler SetNext(IHandler handler) => _next = handler;
    public virtual void Handle(object request)
    {
        if (_next != null)
            _next?.Handle(request);
        else
            WriteLine($"No handler for request {request}");
    }
}
class Leaf(string name) : Component
{
    public override void Excute() => WriteLine($"{name} Do some work");
    public override void Handle(object request)
    {
        if (CanHandle())
            WriteLine($"{this.GetType().Name} handle request: {request}");
        else
        {
            WriteLine($"{this.GetType().Name} trafer request to parent");
            base.Handle(request);
        }
    }
    private bool CanHandle() => false;
}

class Composite(string name) : Component
{
    List<Component> _components = new List<Component>();
    public void Add(Component component)
    {
        _components.Add(component);
        component.SetNext(this);
    }

    public void Remove(Component component)
    {
        _components.Remove(component);
        component.SetNext(null);
    }

    public List<Component> GetChildren() => _components.ToList();

    public override void Excute()
    {
        WriteLine($"Composite {name} execute.");
        _components.ForEach(x => x.Excute());
    }
    public override void Handle(object request)
    {
        if (CanHandle())
            WriteLine($"{this.GetType().Name} handle request: {request}");
        else
            base.Handle(request);
    }
    private bool CanHandle() => true;
}

class Client
{
    public void Run()
    {
        var leaf1 = new Leaf("Leaf1");
        var leaf2 = new Leaf("Leaf2");
        var composite = new Composite("Composite01");
        composite.Add(leaf1);
        composite.Add(leaf2);
        composite.Excute();

        WriteLine("\nTry handle request.");
        leaf1.Handle("request");
    }
}