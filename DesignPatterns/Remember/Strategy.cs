using static System.Console;
namespace DesignPatterns.Remember.Strategy;

class Context
{
    IStrategry _strategry;
    public void SetStrategy(IStrategry strategry) => _strategry = strategry;
    public void DoSomething(string data) => _strategry?.Execute(data);
}

interface IStrategry
{
    void Execute(string data);
}

class ConcreteStrategryA : IStrategry
{
    public void Execute(string data) => WriteLine($"Execute StrategryA with {data}");
}
class ConcreteStrategryB : IStrategry
{
    public void Execute(string data) => WriteLine($"Execute StrategryB with {data}");
}

class Client
{
    public void Run()
    {
        var context = new Context();

        WriteLine("Please enter 'A' or 'B'");

        if (Console.ReadLine() == "A")
            context.SetStrategy(new ConcreteStrategryA());
        else
            context.SetStrategy(new ConcreteStrategryB());
        
        context.DoSomething("456");
    }
}