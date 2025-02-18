using System.Drawing;
using System.Xml.Linq;
using static System.Console;
namespace DesignPatterns.RelatePatterns.CompositeFlyweight;

class ComponentType(string color, string texture)
{
    public void Render() => WriteLine($"Rendering component {color}-{texture}");
}
class Factory
{
    public static Dictionary<string, ComponentType> _components = new Dictionary<string, ComponentType>();
    public static string KeyGen(string color, string texture) => $"{color}-{texture}";
    public static ComponentType GetComponentType(string color, string texture)
        => _components.TryGetValue(KeyGen(color, texture), out ComponentType? component)
        ? component : _components[KeyGen(color, texture)] = new ComponentType(color, texture);
}
interface IComponent
{
    void Execute();
}

abstract class Component : IComponent
{
    private protected ComponentType _componentType;
    public Component(ComponentType componentType) => _componentType = componentType;
    //Factory.GetComponentType(color, texture);
    public abstract void Execute();
}
class Leaf : Component
{
    private string _name;
    public Leaf(string name, ComponentType componentType) : base(componentType)
        => _name = name;
    public override void Execute()
    {
        WriteLine($"{_name} Do some work");
        _componentType.Render();
    }
}
class Composite : Component
{
    private string _name;
    public Composite(string name, ComponentType componentType) : base(componentType)
        => _name = name;
    List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();

    public override void Execute()
    {
        WriteLine($"Comnposite {_name} execute.");
        _componentType.Render();

        _components.ForEach(x => x.Execute());
    }
}

class Client
{
    public void Run()
    {
        var redStriped = Factory.GetComponentType("red", "striped");

        var leaf1 = new Leaf("Leaf1", redStriped);
        var leaf2 = new Leaf("Leaf2", redStriped);
        var composite = new Composite("composite01", redStriped);
        composite.Add(leaf1);
        composite.Add(leaf2);
        var root = new Composite("rootComposite", redStriped);
        root.Add(composite);
        root.Execute();
    }
}