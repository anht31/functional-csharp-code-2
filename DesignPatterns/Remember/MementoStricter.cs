using static System.Console;
namespace DesignPatterns.Remember.MementoStricter;

interface IOriginator
{
    IMemento Save();
    void SetState(string newState);
}
class ConcreteOriginator(string state) : IOriginator
{
    public IMemento Save() => new ConcreteMemento(this, state);
    public void SetState(string newState) 
        => WriteLine($"ConcreateOriginator state {state = newState}");
}

interface IMemento
{
    void Restore();
}
class ConcreteMemento(IOriginator originator, string state) : IMemento
{
    public void Restore() => originator.SetState(state);
}

class Caretaker(IOriginator originator)
{
    Stack<IMemento> mementos = new Stack<IMemento>();
    public void Backup()
    {
        var memento = originator.Save();
        mementos.Push(memento);
    }
    public void Restore()
    {
        var memento = mementos.Pop();
        memento.Restore();
    }
}

class Client()
{
    public void Run()
    {
        var originator = new ConcreteOriginator("A");
        var caretaker = new Caretaker(originator);

        caretaker.Backup();
        originator.SetState("B");
        caretaker.Backup();
        originator.SetState("C");

        WriteLine("Begin Undo sequence...");
        caretaker.Restore();
        caretaker.Restore();
    }
}