
using static System.Console;
namespace DesignPatterns.RelatePatterns.PrototypeComposite;

interface IPrototype
{
    IComponent Clone();
}
interface IComponent : IPrototype
{
    void Excute();
}

class Leaf : IComponent
{
    private string _name;
    public Leaf(string name) => _name = name; 
    public void Excute() => WriteLine($"Leaf {_name} do some work");
    public IComponent Clone() => new Leaf(this);
    private Leaf(Leaf leaf) => _name = leaf._name;
}

class Composite : IComponent
{
    private string _name;
    List<IComponent> _components = new List<IComponent>();

    public Composite(string name) => _name = name;
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();
    public void Excute()
    {
        WriteLine($"Composite {_name} execute.");
        _components.ForEach(x => x.Excute());
    }

    public IComponent Clone() => new Composite(this);
    private Composite(Composite composite)
    {
        _name = composite._name;
        _components = composite._components.Select(x => x.Clone()).ToList();
    }
}

class Client
{
    public void Run()
    {
        var leaf1 = new Leaf("Leaf01");
        var leaf2 = new Leaf("Leaf02");
        var composite = new Composite("Composite01");
        composite.Add(leaf1);
        composite.Add(leaf2);
        var parrent = new Composite("rootComposite");
        parrent.Add(composite);
        parrent.Excute();

        WriteLine("\nClone Composite...");
        var parrentClone = parrent.Clone();
        parrentClone.Excute();
    }
}