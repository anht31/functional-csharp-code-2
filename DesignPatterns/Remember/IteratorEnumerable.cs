using System.Collections;
using static System.Console;
namespace DesignPatterns.Remember.IteratorEnumerable;

interface IIterator : IEnumerator
{
    bool HasMore();
}
class ConcreteIterator : IIterator
{
    private int _position = -1;
    private IIteratorCollecion _collecions;
    public ConcreteIterator(IIteratorCollecion collecions) => _collecions = collecions;
    public bool HasMore() => _position < _collecions.GetItems().Count - 1;

    public object Current => _collecions.GetItems()[_position];
    public bool MoveNext() => HasMore() && ++_position >= 0;
    public void Reset() => _position = 0;
}

interface IIteratorCollecion : IEnumerable {
    IIterator CreateIterator();

    List<int> GetItems();
}
class ConcreteCollection : IIteratorCollecion
{
    List<int> _items = new List<int>();
    public IIterator CreateIterator() => new ConcreteIterator(this);
    
    public void AddItem(int item) => _items.Add(item);
    public List<int> GetItems() => _items;
    public IEnumerator GetEnumerator() => CreateIterator();
}

class Client
{
    public void Run()
    {
        var collecion = new ConcreteCollection();
        collecion.AddItem(1);
        collecion.AddItem(2);
        collecion.AddItem(3);

        foreach (var item in collecion)
        {
            WriteLine(item);
        }
    }
}
