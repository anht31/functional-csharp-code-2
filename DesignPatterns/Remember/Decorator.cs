using static System.Console;
namespace DesignPatterns.Remember.Decorator;

interface IComponent
{
    void Execute();
}

class ConcreteComponent(string name) : IComponent
{
    public void Execute() => WriteLine($"{name} do something");
}

abstract class BaseDecorator : IComponent
{
    IComponent wrappee;
    public BaseDecorator(IComponent component) => wrappee = component;
    public virtual void Execute() => wrappee.Execute();
}
class ConcreteDecorators : BaseDecorator
{
    private string _name;
    public ConcreteDecorators(IComponent component, string name) : base(component)
    {
        _name = name;
    }
    public void Extra() => WriteLine($"{_name} adding effect...");
    public override void Execute()
    {
        base.Execute();
        Extra();
    }
}

class Client
{
    public void Run()
    {
        IComponent concreteComponent = new ConcreteComponent("ConcreteComponent");
        IComponent concreteDecorators = new ConcreteDecorators(concreteComponent, "ConcreteDecorators");
        IComponent anotherConcreteDecorators = new ConcreteDecorators(concreteDecorators, "anotherConcreteDecorators");
        anotherConcreteDecorators.Execute();
    }
}