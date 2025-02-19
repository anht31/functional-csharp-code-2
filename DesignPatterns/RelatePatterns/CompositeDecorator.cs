using System.Xml.Linq;
using static System.Console;
namespace DesignPatterns.RelatePatterns.CompositeDecorator;

interface IComponent
{
    void Execute();
}

class Leaf(string name) : IComponent
{
    public virtual void Execute()
    {
        WriteLine($"{name} Do some work");
    }
}

class Composite(string name) : IComponent
{
    List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();

    public virtual void Execute()
    {
        WriteLine($"Comnposite {name} execute.");
        _components.ForEach(x => x.Execute());
    }
}

class BaseDecorator : IComponent
{
    private protected IComponent wrappee;
    public BaseDecorator(IComponent component) => wrappee = component;
    public virtual void Execute()
    {
        WriteLine($"BaseDecorator {wrappee.GetType().Name} execute.");
        wrappee.Execute();
    }
}
class ConcreteDecorators : BaseDecorator
{
    public ConcreteDecorators(IComponent component) : base(component) { }
    public override void Execute()
    {
        Extra();
        base.Execute();
    }
    public void Extra() => WriteLine($"\nConcreteDecorators {wrappee.GetType().Name} do extra.");
}

class Client
{
    public void Run()
    {
        var leaf1 = new Leaf("Leaf1");
        var leaf2 = new Leaf("Leaf2");
        var leafDecorator = new ConcreteDecorators(new Leaf("LeafDecorator"));
        var conpositeDecorator = new ConcreteDecorators(new Composite("CompositeDecorator"));
        var composite = new Composite("composite01");
        composite.Add(leaf1);
        composite.Add(leaf2);
        composite.Add(leafDecorator);
        composite.Add(conpositeDecorator);
        var root = new Composite("rootComposite");
        root.Add(composite);
        root.Execute();
    }
}
