using static System.Console;
namespace DesignPatterns.RelatePatterns.AbstractFactorySingletons;

interface IAbstractFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}
class WindowFactory : IAbstractFactory
{
    private static WindowFactory? _instance;
    private WindowButton? _buttonInstance;
    private WindowCheckbox? _checkboxInstance;
    private WindowFactory() { }
    public static WindowFactory GetInstance() => _instance ??= new();
    public IButton CreateButton() => _buttonInstance ??= new WindowButton();
    public ICheckbox CreateCheckbox() => _checkboxInstance ??= new WindowCheckbox();
}
class MacFactory : IAbstractFactory
{
    private static MacFactory? _instance;
    private MacButton? _buttonInstance;
    private MacCheckbox? _checkboxInstance;
    private MacFactory() { }
    public static MacFactory GetInstance() => _instance ??= new();
    public IButton CreateButton() => _buttonInstance ??= new MacButton();
    public ICheckbox CreateCheckbox() => _checkboxInstance ??= new MacCheckbox();
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

class Client
{
    IAbstractFactory Config(string os = "Window") => os switch
    {
        "Window" => WindowFactory.GetInstance(),
        "Mac" => MacFactory.GetInstance(),
        _ => throw new NotImplementedException(),
    };
    public void Run()
    {
        IAbstractFactory factory = Config();
        factory.CreateButton().Render();
        factory.CreateButton().Render();
        factory.CreateCheckbox().Render();
    }
}