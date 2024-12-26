using static System.Console;
namespace DesignPatterns.Remember.Composite;

interface IComponent
{
    void Excute();
}

class Leaf(string name) : IComponent
{
    public void Excute() => WriteLine($"{name} Do some work");
}

class Composite : IComponent
{
    List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();

    public void Excute() => _components.ForEach(x => x.Excute());
}

class Client
{
    public void Run()
    {
        var leaf1 = new Leaf("Leaf1");
        var leaf2 = new Leaf("Leaf2");
        var composite = new Composite();
        composite.Add(leaf1);
        composite.Add(leaf2);
        composite.Excute();
    }
}