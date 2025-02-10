using static System.Console;
namespace DesignPatterns.RelatePatterns.BuilderComposite;

interface IComponent {
    void Excute();
}
class Leaf(string name) : IComponent {
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

interface IBuilder {
    void Reset();
    void BuildStepA();
    void BuildStepB();
    void BuildStepZ();
}
class ConcreteBuilder : IBuilder
{
    private IComponent? _component;
    public void BuildStepA() => _component = new Composite();

    public void BuildStepB()
    {
        var leaf1 = new Leaf("Leaf1");
        if (_component is Composite composite)
            composite.Add(leaf1);
    }

    public void BuildStepZ()
    {
        var leaf2 = new Leaf("Leaf2");
        if (_component is Composite composite)
            composite.Add(leaf2);
    }
    public void Reset() => _component = new Composite();
}



class Client
{
    public void Run()
    {
        var leaf2 = new Leaf("Leaf1");
        var leaf2 = new Leaf("Leaf2");
        var composite = new Composite();
        composite.Add(leaf1);
        composite.Add(leaf2);
        composite.Excute();
    }
}