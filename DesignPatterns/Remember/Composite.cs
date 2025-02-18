using System.Xml.Linq;
using static System.Console;
namespace DesignPatterns.Remember.Composite;

interface IComponent
{
    void Execute();
}

class Leaf(string name) : IComponent
{
    public void Execute() => WriteLine($"{name} Do some work");
}

class Composite(string name) : IComponent
{
    List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();

    public void Execute()
    {
        WriteLine($"Comnposite {name} execute.");
        _components.ForEach(x => x.Execute());
    }
}

class Client
{
    public void Run()
    {
        var leaf1 = new Leaf("Leaf1");
        var leaf2 = new Leaf("Leaf2");
        var composite = new Composite("composite01");
        composite.Add(leaf1);
        composite.Add(leaf2);
        var root = new Composite("rootComposite");
        root.Add(composite);
        root.Execute();
    }
}