using static System.Console;
namespace DesignPatterns.Remember.FactoryMethod;

abstract class Factory
{
    public abstract IProduct Create();
    public void Render() => Create().Do();
}
class FactoryWindow : Factory
{
    public override IProduct Create() => new ConcreateWindow();
}
class FactoryMac : Factory
{
    public override IProduct Create() => new ConcreteMac();
}

interface IProduct
{
    void Do();
}
class ConcreateWindow : IProduct
{
    public void Do() => WriteLine($"{this.GetType()} Do Something");
}
class ConcreteMac : IProduct
{
    public void Do() => WriteLine($"{this.GetType()} Do Something");
}


class App
{
    Factory Config(string os = "Window") => os switch
    {
        "Window" => new FactoryWindow(),
        "Mac" => new FactoryMac(),
        _ => throw new NotImplementedException()
    };
    public void Run()
    {
        Factory factory = Config("Mac");
        factory.Render();
    }
}