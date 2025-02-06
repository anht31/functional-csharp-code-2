using System.Collections;
using static System.Console;
namespace DesignPatterns.RelatePatterns.FactoryMethodIterator;

class Factory
{

}
interface IIterator
{
    int GetNext();
    bool HasMore();
    void Reset();
}
class ConcreteListIterator : IIterator
{
    private int _position = -1;
    private IIteratorCollecion _collecion;
    private List<int> _cache;
    public ConcreteListIterator(IIteratorCollecion collecion) => _collecion = collecion;
    public bool HasMore() => (LazyInit() != null) && _position < _cache.Count - 1;
    public int GetNext() => _cache[++_position];
    private object LazyInit() => _cache ??= _collecion.GetItems();
    public void Reset()
    {
        this._position = -1;
        this._cache = null;
    }
}
class ConcreteStackIterator : IIterator
{
    private int _position = -1;
    private IIteratorCollecion _collection;
    private List<int> _cache;
    public ConcreteStackIterator(IIteratorCollecion collecion) => _collection = collecion;
    public int GetNext() => _cache[++_position];
    public bool HasMore() => (LazyInit() != null) && _position < (_cache.Count - 1);
    private object LazyInit() => _cache ?? (_cache = _collection.GetItems());
    public void Reset()
    {
        this._position = -1;
        this._cache = null;
    }
}

interface IIteratorCollecion
{
    IIterator CreateIterator();

    List<int> GetItems();
    void AddItem(int item);
}
class ListCollection : IIteratorCollecion
{
    List<int> _items = new List<int>();
    public IIterator CreateIterator() => new ConcreteListIterator(this);

    // Helper func
    public void AddItem(int item) => _items.Add(item);
    public List<int> GetItems() => _items;
}
class StackCollection : IIteratorCollecion
{
    Stack<int> _items = new Stack<int>();
    public IIterator CreateIterator() => new ConcreteStackIterator(this);

    public List<int> GetItems() => _items.ToList();
    public void AddItem(int item) => _items.Push(item);
}

class Client
{
    private string GetCollectionType() => "stack_int";
    public void Run()
    {
        IIteratorCollecion collection = GetCollectionType() switch
        {
            "list_int" => new ListCollection(),
            "stack_int" => new StackCollection(),
            _ => throw new NotImplementedException()
        };

        InitData(collection);
    }

    private void InitData(IIteratorCollecion collection)
    {
        collection.AddItem(1);
        collection.AddItem(2);
        var iterator = collection.CreateIterator();
        while (iterator.HasMore())
            WriteLine(iterator.GetNext());

        WriteLine("\nReset and number 3");
        iterator.Reset();
        collection.AddItem(3);
        while (iterator.HasMore())
            WriteLine(iterator.GetNext());
    }
}