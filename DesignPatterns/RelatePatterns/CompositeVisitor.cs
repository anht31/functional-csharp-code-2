using static System.Console;
namespace DesignPatterns.RelatePatterns.CompositeVisitor;

interface IVisitor
{
    void VisitLeaf(Leaf leaf);
    void VisitComposite(Composite composite);
}
class RenderVisitor : IVisitor
{
    public void VisitComposite(Composite composite) => WriteLine($"Rendering Composite {composite.Name}");
    public void VisitLeaf(Leaf leaf) => WriteLine($"Rendering Leaf {leaf.Name}");
}

interface IComponent
{
    void Execute(IVisitor visitor);
    void Accept(IVisitor visitor);
}

class Leaf(string name) : IComponent
{
    public void Accept(IVisitor visitor) => visitor.VisitLeaf(this);

    public void Execute(IVisitor visitor)
    {
        WriteLine($"Leaf {name} execute");
        this.Accept(visitor);
    }

    public string Name => name;
}

class Composite(string name) : IComponent
{
    List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();

    public void Execute(IVisitor visitor)
    {
        WriteLine($"Composite {name} execute");
        this.Accept(visitor);
        _components.ForEach(x => x.Execute(visitor));
    }

    public void Accept(IVisitor visitor) => visitor.VisitComposite(this);
    public string Name => name;
}

class Client
{
    public void Run()
    {
        RenderVisitor visitor = new RenderVisitor();

        var leaf1 = new Leaf("Leaf1");
        var leaf2 = new Leaf("Leaf2");
        var composite = new Composite("Composite01");
        composite.Add(leaf1);
        composite.Add(leaf2);
        var root = new Composite("rootComposite");
        root.Add(composite);
        root.Execute(visitor);
    }
}