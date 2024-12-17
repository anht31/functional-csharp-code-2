using static System.Console;
namespace DesignPatterns.Study.ExampleMementoStricter;

public interface IOriginator
{
    IMemento Save();
}

class Originator : IOriginator
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

    public IMemento Save() => new ConcreteMemento(this, this._state);

    // Appropriate setters
    public void SetState(string state) => _state = state;
}

// Restrict access, only declare methods related to the memento's metadata.
public interface IMemento
{
    string GetName();
    DateTime GetDate();
    void Restore();
}

class ConcreteMemento : IMemento
{
    private Originator _originator;
    private string _state;
    private DateTime _date;
    public ConcreteMemento(Originator originator, string state)
    {
        _originator = originator;
        this._state = state;
        this._date = DateTime.Now;
    }
    // The Originator uses this method when restoring its state.
    public void Restore()
    {
        _originator.SetState(this._state);
        Write($"Originator: My state has changed to: {_state}");
    }
    // The rest of the methods are used by the Caretaker to display metadata.
    public string GetName() => $"{this._date} / ({this._state.Substring(0, 9)})...";
    public DateTime GetDate() => this._date;
}

// caretaker class becomes independent from the originator because
//  the restoration method is now defined in the memento class
class Caretaker
{
    private Originator _originator = null;
    private Stack<IMemento> _mementos = new Stack<IMemento>();

    public Caretaker(Originator originator) => this._originator = originator;

    public void Backup()
    {
        WriteLine("\nCaretaker: Saving Originator's state...");
        this._mementos.Push(this._originator.Save());
    }

    public void Undo()
    {
        if (this._mementos.Count == 0)
            return;

        var memento = this._mementos.Pop();
        WriteLine("Caretaker: Restoring state to: " + memento.GetName());

        try
        {
            memento.Restore();
        }
        catch (Exception)
        {
            this.Undo(); // Next in stack
        }
    }

    public void ShowHistory()
    {
        WriteLine("Caretaker: Here's the list of mementos:");
        this._mementos.ToList().ForEach(memento => WriteLine(memento.GetName()));
    }
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

        WriteLine();
        caretaker.ShowHistory();

        WriteLine("\nClient: Now, let's rollback!\n");
        caretaker.Undo();
        WriteLine("\n\nClient: Once more!\n");
        caretaker.Undo();
    }
}