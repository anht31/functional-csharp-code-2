using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Console;
namespace DesignPatterns.RelatePatterns.BuilderComposite;

interface IComponent {
    void Excute();
}
class Leaf(string name) : IComponent {
    public void Excute() => WriteLine($"{name} Do some work");
}
class Composite(string name) : IComponent
{
    List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();
    public string Name => name;
    public void Excute()
    {
        WriteLine($"Excute {name} Composite");
        _components.ForEach(x => x.Excute());
    }
}

interface IBuilder {
    void Reset(string name);
    void BuildNode(string name);
    void BuildLeaf(string name);
    void SetCurrent(string name);
}
class ConcreteBuilder : IBuilder
{
    private IComponent _component; // TopLevel Composite
    private Composite _current;
    public ConcreteBuilder(string name) => _component = _current = new Composite(name);
    public void BuildNode(string name)
    {
        var newNode = new Composite(name);
        _current.Add(newNode);
        _current = newNode;
    }

    public void BuildLeaf(string name) => _current.Add(new Leaf(name));
    public void Reset(string name) => _component = _current = new Composite(name);
    public void SetCurrent(string name)
    {
        var component = FindComposite(_component as Composite, name);
        if (component is Composite composite)
            _current = composite;
    }

    private IComponent FindComposite(Composite composite, string name)
    {
        foreach (var item in composite.GetChildren())
        {
            if (item is Composite compositeItem)
                return compositeItem.Name == name ? item : FindComposite(compositeItem, name);
        }
        return null;
    }
    public IComponent GetComponent() => _component;
}
class Director
{
    private IBuilder? _builder;
    public Director(IBuilder builder) => _builder = builder;
    public void SetBuilder(IBuilder builder) => _builder = builder;
    public void Build()
    {
        _builder.BuildNode("node-lv1");
        _builder.BuildNode("node-lv2");
        _builder.BuildLeaf("leaf-21");
        _builder.BuildLeaf("leaf-22");
        _builder.SetCurrent("node-lv1");
        _builder.BuildLeaf("leaf-11");
    }
}

class Client
{
    public void Run()
    {
        var builder = new ConcreteBuilder("root");
        var director = new Director(builder);
        director.Build();
        var product = builder.GetComponent();
        product.Excute();
    }
}