using static System.Console;
using System.Collections.Generic;

namespace DesignPatterns.Study.ChainOfResponsibility;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    object Handle(object request);
}

public abstract class AbstractHandler : IHandler
{
    private IHandler _nextHandler;
    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual object Handle(object request)
        => _nextHandler != null
            ? _nextHandler.Handle(request)
            : null;
}

class MonkeyHandler : AbstractHandler
{
    public override object Handle(object request)
        => request as string == "Banana"
            ? $"Monkey: I'll eat the {request.ToString()}.\n"
            : base.Handle(request);
}

class SquirrelHandler : AbstractHandler
{
    public override object Handle(object request)
        => request.ToString() == "Nut"
            ? $"Squirrel: I'll eat the {request.ToString()}.\n"
            : base.Handle(request);
}

class DogHandler : AbstractHandler
{
    public override object Handle(object request)
        => request.ToString() == "MeatBall"
            ? $"Dog: I'll eat the {request.ToString()}.\n"
            : base.Handle(request);
}

public class Client
{
    public Client(AbstractHandler handler)
    {
        foreach (var food in new List<string> { "Nut", "Banana", "Cup of coffee" })
        {
            WriteLine($"Client: Who wants a {food}?");
            var result = handler.Handle(food);
            WriteLine(result != null ? result : $"{food} was left untouched.");
        }
    }
}

public class App
{
    public void Run()
    {
        var monkey = new MonkeyHandler();
        var squirrel = new SquirrelHandler();
        var dog = new DogHandler();

        monkey.SetNext(squirrel)
                .SetNext(dog);

        WriteLine("Chain: Monkey > Squirrel > Dog\n");
        new Client(monkey);
    }
}
