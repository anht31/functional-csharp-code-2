using static System.Console;
namespace Observer;

class Publisher
{
    private List<ISubscriber> subsribers = new List<ISubscriber>();

    public void NotifySubscribers() => subsribers.ForEach(x => x.Update("New device release"));

    public void Subcribe(ISubscriber subcriber) => subsribers.Add(subcriber);
    public void UnSubcribe(ISubscriber subcriber) => subsribers.Remove(subcriber);
}

public interface ISubscriber {
    void Update(string message);
}
class SubscriberA : ISubscriber {
    public void Update(string message) => WriteLine($"SubscriberA get notify about: {message}");
}
class SubscriberB : ISubscriber {
    public void Update(string message) => WriteLine($"SubscriberB get notify about: {message}");
}
class SubscriberC : ISubscriber {
    public void Update(string message) => WriteLine($"SubscriberC get notify about: {message}");
}

class App
{
    public void Run() {
        var a = new SubscriberA();
        var b = new SubscriberB();
        var c = new SubscriberC();

        var publisher = new Publisher();
        publisher.Subcribe(a);
        publisher.Subcribe(c);
        publisher.NotifySubscribers();

        WriteLine("UnSubscribe C");
        publisher.UnSubcribe(c);
        publisher.NotifySubscribers();
    }
}
