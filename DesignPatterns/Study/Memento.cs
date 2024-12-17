using static System.Console;
namespace DesignPatterns.Study.Memento;

class Originator
{
    private string state;
    public Originator(string state) => this.state = state;
    public object Save() => new Memento(state);
    public void Restore(object memento)
    {
        // can't access from outside: Originator.Memento
        if (memento is Memento m) state = m.GetState();
        else throw new ArgumentException("Invalid Memento");
    }
    public void ShowState() => WriteLine($"Current State: {state}");
    public void DoSomething() => state = (int.Parse(state) + 1).ToString();
    // Limit access for Originator only
    private class Memento
    {
        private readonly string state;
        internal Memento(string state) => this.state = state;
        // Giới hạn truy cập trạng thái chỉ cho Originator
        internal string GetState() => state;
    }
}

class Caretaker
{
    private readonly Stack<object> history = new(); // use object as interface for Memento
    public void Save(Originator originator) => history.Push(originator.Save());
    public void Undo(Originator originator)
    {
        if (history.Count > 0)
        {
            var memento = history.Pop();
            originator.Restore(memento);
        }
        else WriteLine("No states to restore.");
    }
}

public class App
{
    public void Run()
    {
        Originator originator = new Originator("1");
        Caretaker caretaker = new Caretaker();
        originator.ShowState();
        caretaker.Save(originator);

        caretaker.Save(originator);
        WriteLine("\nDoSomething: +1");
        originator.DoSomething();
        originator.ShowState();

        caretaker.Undo(originator);
        WriteLine("\nUndo");
        originator.ShowState();
    }
}