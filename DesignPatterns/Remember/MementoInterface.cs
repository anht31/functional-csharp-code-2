using static System.Console;
namespace DesignPatterns.Remember.MementoInterface;

class Originator
{
    string state = string.Empty;
    public void UpdateState(string value) 
        => WriteLine($"Originator state -> {state = value}");
    public IMemento Save() => new ConcreteMemento(state);
    public void Restore(IMemento memento) 
        => WriteLine($"Originator state -> {state = memento.GetState()}");
}

interface IMemento
{
    string GetState();
}

public class ConcreteMemento(string state) : IMemento
{
    public string GetState() => state;
}

class Caretaker(Originator originator)
{
    Stack<IMemento> _history = new Stack<IMemento>();
    public void Backup()
    {
        var memento = originator.Save();
        _history.Push(memento);
    }
    public void Undo()
    {
        var memento = _history.Pop();
        originator.Restore(memento);
    }
}

class Client
{
    public void Run()
    {
        var originator = new Originator();
        var caretaker = new Caretaker(originator);

        originator.UpdateState("A");
        caretaker.Backup();

        originator.UpdateState("B");
        caretaker.Backup();

        originator.UpdateState("C");

        WriteLine("Begin Undo sequence...");
        caretaker.Undo();
        caretaker.Undo();
    }
}