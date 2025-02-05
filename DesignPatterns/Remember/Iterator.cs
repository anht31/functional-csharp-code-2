using System.Collections;
using static System.Console;
namespace DesignPatterns.Remember.Iterator;

interface IIterator
{
    int GetNext();
    bool HasMore();
}
class ConcreteIterator : IIterator
{
    private int _position = -1;
    private IIteratorCollecion _collecions;
    private List<int> _cache;
    public ConcreteIterator(IIteratorCollecion collecions) => _collecions = collecions;
    public bool HasMore() => (LazyInit() != null) && _position < _cache.Count - 1;
    public int GetNext() => _cache[++_position];
    //private bool LazyInit()
    //{
    //    if (_cache == null)
    //        _cache = _collecions.GetItems();
    //    return true;
    //}
    private object LazyInit() => _cache ??= _collecions.GetItems() ;
}

interface IIteratorCollecion {
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
        while(iterator.HasMore())
            WriteLine(iterator.GetNext());
    }
}
