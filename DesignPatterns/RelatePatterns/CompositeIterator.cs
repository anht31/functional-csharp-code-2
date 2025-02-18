using System.Collections;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
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
    public IComponent GetNext() => _cache[++_position];
    public bool HasMore() => (LazyInit() != null) && _position < (_cache.Count - 1);
    private object LazyInit() => _cache ??= _collection.GetItems().ToList();
}

interface ICollection
{
    IIterator CreateIterator();
    IEnumerable<IComponent> GetItems();
}
interface IComponent : ICollection
{
    void Excute();
    void Display();
}
class Leaf(string name) : IComponent
{
    public void Excute() => WriteLine($"{name} Do some work");
    public void Display() => WriteLine($"{name} Do some work");
    public IIterator CreateIterator() => new ConcreteIterator(this);
    public IEnumerable<IComponent> GetItems()
    {
        yield return this;
    }
}

class Composite(string name) : IComponent
{
    private List<IComponent> _components = new List<IComponent>();
    public void Add(IComponent component) => _components.Add(component);
    public void Remove(IComponent component) => _components.Remove(component);
    public List<IComponent> GetChildren() => _components.ToList();

    public void Excute()
    {
        WriteLine($"Composite {name} Execute.");
        _components.ForEach(x => x.Excute());
    }
    public void Display() => WriteLine($"Composite {name} Execute.");

    public IIterator CreateIterator() => new ConcreteIterator(this);
    public IEnumerable<IComponent> GetItems()
    {
        yield return this;
        foreach (var component in _components)
        {
            // this foreach support for yeild return only
            foreach (var child in component.GetItems())
                yield return child;
        }
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

        WriteLine("\nIteratorr in Tree.\n");
        var iterator = compositeRoot.CreateIterator();
        while (iterator.HasMore())
        {
            iterator.GetNext().Display();
        }
    }
}
