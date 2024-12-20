using static System.Console;
namespace DesignPatterns.Remember.AbstractFactory;

interface IAbstractFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}
class WindowFactory : IAbstractFactory
{
    public IButton CreateButton() => new WindowButton();

    public ICheckbox CreateCheckbox() => new WindowCheckbox();
}
class MacFactory : IAbstractFactory
{
    public IButton CreateButton() => new MacButton();

    public ICheckbox CreateCheckbox() => new MacCheckbox();
}

interface IButton
{
    void Render();
}
class WindowButton : IButton
{
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}
class MacButton : IButton
{
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}

interface ICheckbox
{
    void Render();
}
class WindowCheckbox : ICheckbox
{
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}
class MacCheckbox : ICheckbox
{
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}

class App
{
    IAbstractFactory Config(string os = "Window") => os switch
    {
        "Window" => new WindowFactory(),
        "Mac" => new MacFactory(),
        _ => throw new NotImplementedException(),
    };
    public void Run()
    {
        IAbstractFactory factory = Config();
        factory.CreateButton().Render();
        factory.CreateCheckbox().Render();
    }
}