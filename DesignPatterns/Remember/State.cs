using static System.Console;
namespace DesignPatterns.Remember.State;

class Context
{
    private IState _state;
    public Context(IState state)
    {
        _state = state;
        _state.SetContext(this);
    }

    public void SetState(IState state)
    {
        WriteLine($"Context chage state: {state.GetType().Name}");
        _state = state;
        _state.SetContext(this);
    }

    public void DoThis() => _state.DoThis();
    public void DoThat() => _state.DoThat();
}

interface IState
{
    void SetContext(Context context);
    void DoThis();
    void DoThat();
}

class ConcreteStateA() : IState
{
    Context _context;
    public void SetContext(Context context) => _context = context;
    public void DoThis()
    {
        WriteLine($"{this.GetType().Name} do This.");
        WriteLine("Change State.");
        _context.SetState(new ConcreteStateB());
    }
    public void DoThat()
    {
        WriteLine($"{this.GetType().Name} do That.");
        WriteLine("Change State.");
        _context.SetState(new ConcreteStateB());
    }

}

class ConcreteStateB() : IState
{
    Context _context;
    public void SetContext(Context context) => _context = context;
    public void DoThis()
    {
        WriteLine($"{this.GetType().Name} do This.");
        WriteLine("Change State.");
        _context.SetState(new ConcreteStateA());
    }
    public void DoThat()
    {
        WriteLine($"{this.GetType().Name} do That.");
        WriteLine("Change State.");
        _context.SetState(new ConcreteStateA());
    }

}


class Client
{
    public void Run()
    {
        var initialState = new ConcreteStateA();
        var context = new Context(initialState);
        context.DoThis();
        context.DoThat();
    }
}