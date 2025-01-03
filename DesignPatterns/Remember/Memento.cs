using static System.Console;
namespace DesignPatterns.Remember.Memento;

class Originator
{
    string state = string.Empty;
    public void UpdateState(string value)
    {
        state = value;
        WriteLine($"Originator state -> {state}");
    }

    public object Save() => new Memento(state);
    public void Restore(object memento)
    {
        state = memento is Memento m ? m.GetState() : state;
        WriteLine($"Originator state -> {state}");
    }

    public class Memento
    {
        private string _state;
        public Memento(string state) => _state = state;
        public string GetState() => _state;
    }
}

class Caretaker
{
    Originator _originator;
    public Caretaker(Originator originator) => _originator = originator;
    Stack<object> _history = new Stack<object>();
    public void Backup()
    {
        var memento = _originator.Save();
        _history.Push(memento);
    }
    public void Undo()
    {
        var memento = _history.Pop();
        _originator.Restore(memento);
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