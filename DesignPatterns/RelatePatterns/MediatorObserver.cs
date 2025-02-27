using static System.Console;
namespace DesignPatterns.RelatePatterns.MediatorObserver;

interface IMediator
{
    void Notify(ComponentObserver concreteObject, string message);
}
class MediatorPublisher : IMediator
{
    private List<ComponentObserver> subcribers = new ();
    public void Subscribe(ComponentObserver observer)
    {
        observer.SetMediator(this);
        subcribers.Add(observer);
    }

    public void UnSubscibe(ComponentObserver observer)
    {
        if (!subcribers.Any(x => x == observer))
        {
            subcribers.Remove(observer);
            observer.SetMediator(null);
        }
    }

    public void Notify(ComponentObserver sender, string message)
    {
        foreach(var component in subcribers)
        {
            if (component != sender)
            {
                component.Reaction(message);
            }
        }
    }
}

abstract class ComponentObserver
{
    protected IMediator _mediator;
    public ComponentObserver(IMediator mediator = null) => _mediator = mediator;
    public void SetMediator(IMediator mediator) => _mediator = mediator;
    public abstract void Reaction(string message);
}
class ComponentSender : ComponentObserver
{
    public void Operation() => _mediator?.Notify(this, "Sender triggered operation");
    public override void Reaction(string message) => WriteLine($"ComponentSender reacting: {message}");
}
class ComponentA : ComponentObserver
{
    public void Operation() => _mediator?.Notify(this, "ComponentA triggered operation");
    public override void Reaction(string message) => WriteLine($"ComponentA reacting: {message}");
}
class ComponentB : ComponentObserver
{
    public void Operation() => _mediator?.Notify(this, "ComponentB triggered operation");
    public override void Reaction(string message) => WriteLine($"ComponentB reacting: {message}");
}

class Client
{
    public void Run()
    {
        var mediator = new MediatorPublisher();
        var sender = new ComponentSender();
        ComponentA componentA = new ComponentA();
        ComponentB componentB = new ComponentB();

        mediator.Subscribe(sender);
        mediator.Subscribe(componentA);
        mediator.Subscribe(componentB);

        sender.Operation();
        WriteLine();
        componentA.Operation();
    }
}