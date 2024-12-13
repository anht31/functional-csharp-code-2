using static System.Console;
namespace ExampleMemento;

class Originator
{
    private string _state;
    public Originator(string state)
    {
        this._state = state;
        WriteLine("Originator: My initial state is: " + state);
    }
    public void DoSomething()
    {
        WriteLine("Originator: I'm doing something important.");
        this._state = new Random().Next().ToString();
        WriteLine($"Originator: and my state has changed to: {_state}");
    }

    public IMemento Save() => new ConcreteMemento(this._state);

    public void Restore(IMemento memento)
    {
        if (!(memento is ConcreteMemento m))
        {
            throw new Exception("Unknown memento class " + memento.ToString());
        }
        this._state = m.GetState();
        Console.Write($"Originator: My state has changed to: {_state}");
    }
}

// Restrict access, only declare methods related to the memento's metadata.
public interface IMemento
{
    string GetName();
    DateTime GetDate();
}

class ConcreteMemento : IMemento
{
    private string _state;
    private DateTime _date;
    public ConcreteMemento(string state)
    {
        this._state = state;
        this._date = DateTime.Now;
    }
    // The Originator uses this method when restoring its state.
    public string GetState() => this._state;
    // The rest of the methods are used by the Caretaker to display metadata.
    public string GetName() => $"{this._date} / ({this._state.Substring(0, 9)})...";
    public DateTime GetDate() => this._date;
}

// The Caretaker doesn't depend on the Concrete Memento class. Therefore, it
// doesn't have access to the originator's state, stored inside the memento.
// It works with all mementos via the base Memento interface.
class Caretaker
{
    private Originator _originator = null;
    private List<IMemento> _mementos = new List<IMemento>();

    public Caretaker(Originator originator) => this._originator = originator;

    public void Backup()
    {
        WriteLine("\nCaretaker: Saving Originator's state...");
        this._mementos.Add(this._originator.Save());
    }

    public void Undo()
    {
        if (this._mementos.Count == 0)
            return;

        var memento = this._mementos.Last();
        this._mementos.Remove(memento);

        WriteLine("Caretaker: Restoring state to: " + memento.GetName());

        try {
            this._originator.Restore(memento);
        } catch (Exception) {
            this.Undo(); // Next in stack
        }
    }

    public void ShowHistory()
    {
        WriteLine("Caretaker: Here's the list of mementos:");
        this._mementos.ForEach(memento => WriteLine(memento.GetName()));
    }

    // Limit access by use interface
    //public void TryShowLast() => this._mementos.Last().GetState();
}

class Program
{
    public static void Main(string[] args)
    {
        // Client code.
        Originator originator = new Originator("Super-duper-super-puper-super.");
        Caretaker caretaker = new Caretaker(originator);

        caretaker.Backup();
        originator.DoSomething();

        caretaker.Backup();
        originator.DoSomething();

        caretaker.Backup();
        originator.DoSomething();

        WriteLine();
        caretaker.ShowHistory();

        WriteLine("\nClient: Now, let's rollback!\n");
        caretaker.Undo();

        WriteLine("\n\nClient: Once more!\n");
        caretaker.Undo();

        WriteLine("\n\nClient: Once more!\n");
        caretaker.Undo();
        WriteLine();
    }
}