using static System.Console;
namespace DesignPatterns.Remember.Observer;

class Publisher
{
    private List<IObserver> subscribers = new List<IObserver>();
    public void Notify() => subscribers.ForEach(x => x.Update(this));
    public void Subscribe(IObserver observer) => subscribers.Add(observer);
    public void UnSubscribe(IObserver observer) => subscribers.Remove(observer);
}

interface IObserver
{
    void Update(Publisher publisher);
}
class ConcreteObserver(string name) : IObserver
{
    public void Update(Publisher publisher) => WriteLine($"{name} has been Notified");
}

class Client
{
    public void Run()
    {
        Publisher publisher = new Publisher();
        IObserver observerA = new ConcreteObserver("ObserverA");
        IObserver observerB = new ConcreteObserver("ObserverB");
        IObserver observerC = new ConcreteObserver("ObserverC");

        publisher.Subscribe(observerA);
        publisher.Subscribe(observerC);
        publisher.Notify();
    }
}