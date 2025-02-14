
using static System.Console;
namespace DesignPatterns.RelatePatterns.PrototypeDecorator;

interface IPrototype
{
    IComponent Clone();
}
interface IComponent : IPrototype
{
    void Execute();
}

class ConcreteComponent: IComponent
{
    private string _name;
    public ConcreteComponent(string name) => _name = name;
    public void Execute() => WriteLine($"{_name} do something");

    public IComponent Clone() => new ConcreteComponent(this);
    private ConcreteComponent(ConcreteComponent component) => this._name = component._name;
}

abstract class BaseDecorator : IComponent
{
    IComponent wrappee;
    public BaseDecorator(IComponent component) => wrappee = component;
    public virtual void Execute() => wrappee.Execute();
    //public BaseDecorator(IComponent component, bool isClone)
    //    => this.wrappee = isClone && component is ConcreteDecorators decorator
    //        ? decorator.Clone() : component is ConcreteComponent concreteComponent
    //            ? concreteComponent.Clone() : component;

    // BaseDecorator is abstract, thus just Clone for ConcreteComponent, ConcreteDecorators
    public BaseDecorator(IComponent component, bool isClone)
        => this.wrappee = isClone ? component.Clone() : component;
    public abstract IComponent Clone();
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

    public override IComponent Clone() => new ConcreteDecorators(this);
    private ConcreteDecorators(ConcreteDecorators component) : base(component, true)
    {
        this._name = component._name;
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

        WriteLine($"\nClone decorator...");
        var cloneDecorator = anotherConcreteDecorators.Clone();
    }
}
