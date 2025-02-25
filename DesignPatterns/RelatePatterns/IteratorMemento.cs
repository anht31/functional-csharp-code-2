using static System.Console;
namespace DesignPatterns.RelatePatterns.IteratorMemento;

interface IIterator
{
    int GetNext();
    bool HasMore();
    IMemento Save();
    void Restore(IMemento memento);
}
class ConcreteIterator : IIterator
{
    private int _position = -1;
    private IIteratorCollecion _collecions;
    private List<int> _cache;
    public ConcreteIterator(IIteratorCollecion collecions) => _collecions = collecions;
    public bool HasMore() => (LazyInit() != null) && _position < (_cache.Count - 1);
    public int GetNext()
    {
        LazyInit();
        return _cache[++_position];
    }

    private object LazyInit() => _cache ??= _collecions.GetItems();

    public IMemento Save() => new ConcreteMemento(_position, _collecions, _cache);
    public void Restore(IMemento memento)
    {
        var iteratorTupple = memento.Restore();
        _position = iteratorTupple.position;
        _collecions = iteratorTupple.collecions;
        _cache = iteratorTupple.cache;
    }
}

interface IMemento
{
    (int position, IIteratorCollecion collecions, List<int> cache) Restore();
}
class ConcreteMemento(int position, IIteratorCollecion collecions, List<int> cache) : IMemento
{
    public (int position, IIteratorCollecion collecions, List<int> cache) Restore() 
        => (position, collecions, cache);
}

class Caretaker(IIterator originator)
{
    Stack<IMemento> history = new Stack<IMemento>();
    public void Backup()
    {
        var memento = originator.Save();
        history.Push(memento);
    }
    public void Restore()
    {
        var memento = history.Pop();
        originator.Restore(memento);
    }
}

interface IIteratorCollecion
{
    IIterator CreateIterator();

    List<int> GetItems();
}
class ConcreteCollection : IIteratorCollecion
{
    List<int> _items = new List<int>();
    public IIterator CreateIterator() => new ConcreteIterator(this);

    // Helper func
    public void AddItem(int item) => _items.Add(item);
    public List<int> GetItems() => _items;
}

class Client
{
    public void Run()
    {
        var collecion = new ConcreteCollection();
        collecion.AddItem(1);
        collecion.AddItem(2);
        collecion.AddItem(3);

        var iterator = collecion.CreateIterator();
        var caretaker = new Caretaker(iterator);
        caretaker.Backup();

        WriteLine(iterator.GetNext());
        WriteLine();
        caretaker.Restore();

        while (iterator.HasMore())
            WriteLine(iterator.GetNext());
    }
}
