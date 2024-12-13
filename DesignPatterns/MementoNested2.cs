using System.Security.Cryptography.X509Certificates;
using static System.Console;
public class Originator
{
    private string state;

    public Originator(string state) => this.state = state;

    public object Save() => new Memento(state);

    public void Restore(object memento)
    {
        if (memento is Memento m) this.state = m.GetState();
        else throw new ArgumentException("Invalid Memento");
    }

    public void ShowState() => WriteLine($"Current State: {state}");

    public void DoSomething() => state = (int.Parse(state) + 1).ToString();

    // Nested class chỉ có thể được sử dụng bởi Originator
    private class Memento
    {
        private readonly string state;

        internal Memento(string state) => this.state = state;

        internal string GetState() => state;
    }
}