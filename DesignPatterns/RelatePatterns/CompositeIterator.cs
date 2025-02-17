using static System.Console;
namespace DesignPatterns.RelatePatterns.CompositeIterator;

interface IIterator
{
    IComponent GetNext();
    bool HasMore();
}
class ConcreteIterator : IIterator
{
    private int _position = -1;
    private ICollection _collection;
    private List<IComponent> _cache;
    public ConcreteIterator(ICollection collection) => _collection = collection;
    public IComponent GetNext()
    {
        throw new NotImplementedException();
    }

    public bool HasMore()
    {
        throw new NotImplementedException();
    }
}

interface ICollection
{
    IIterator CreateIterator();
    List<IComponent> GetItems();
}
interface IComponent
{
    void Excute();
}

class Leaf(string name) : IComponent
{
    public void Excute() => WriteLine($"{name} Do some work");
}

class Composite(string name) : IComponent
{
    List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();

    public void Excute()
    {
        WriteLine($"Composite {name} Execute.");
        _components.ForEach(x => x.Excute());
    }
}

class Client
{
    public void Run()
    {
        var leaf1 = new Leaf("Leaf11");
        var leaf2 = new Leaf("Leaf12");
        var composite = new Composite("Composite11");
        composite.Add(leaf1);
        composite.Add(leaf2);
        var compositeRoot = new Composite("root");
        compositeRoot.Add(composite);
        compositeRoot.Excute();
    }
}
