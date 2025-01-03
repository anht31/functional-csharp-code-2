using static System.Console;
namespace DesignPatterns.Remember.Mediator;

interface IMediator
{
    void Notify(Component concreteObject, string message);
}
class Mediator : IMediator
{
    private ComponentA _componentA;
    private ComponentB _componentB;
    public Mediator(ComponentA componentA, ComponentB componentB)
    {
        _componentA = componentA;
        _componentA.SetMediator(this);
        _componentB = componentB;
        _componentB.SetMediator(this);
    }
    public void Notify(Component sender, string message)
    {
        switch (sender)
        {
            case ComponentA:
                ReactOnB();
                break;
            case ComponentB:
                ReactOnA();
                ReactOnB();
                break;
        }
    }
    void ReactOnA() => _componentA.Reaction();
    void ReactOnB() => _componentB.Reaction();
}

abstract class Component
{
    protected IMediator _mediator;
    public Component(IMediator mediator = null) => _mediator = mediator;
    public void SetMediator(IMediator mediator) => _mediator = mediator;
}
class ComponentA : Component
{
    public ComponentA(IMediator mediator = null) : base(mediator) { }
    public void OperationA() => _mediator.Notify(this, "A");
    public void Reaction() => WriteLine("Do another on A");
}
class ComponentB : Component
{
    public void OperationB() => _mediator.Notify(this, "B");
    public void Reaction() => WriteLine("Do another on B");
}

class Client
{
    public void Run()
    {
        ComponentA componentA = new ComponentA();
        ComponentB componentB = new ComponentB();
        new Mediator(componentA, componentB);

        WriteLine("Client triggers operation A");
        componentA.OperationA();
        WriteLine("Client triggers operation B");
        componentB.OperationB();
    }
}