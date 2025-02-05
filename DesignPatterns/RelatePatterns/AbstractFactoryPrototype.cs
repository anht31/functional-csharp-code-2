using static System.Console;
namespace DesignPatterns.RelatePatterns.AbstractFactoryPrototype;

interface IButton {
    void Render();
}
interface ICheckbox {
    void Render();
}
class WindowButtons : IButton {
    public void Render() => WriteLine($"WindowButton Rendered");
}
class MacButtons : IButton {
    public void Render() => WriteLine($"MacButtons Rendered");
}
class WindowCheckbox : ICheckbox {
    public void Render() => WriteLine($"WindowCheckbox Rendered");
}
class MacCheckbox : ICheckbox {
    public void Render() => WriteLine($"MacCheckbox Rendered");
}

interface Dialog
{
    IButton CreateButton(string shape, string color);
    ICheckbox CreateCheckbox(string shape, string color);
}
class WindowDialog : Dialog
{
    public IButton CreateButton(string shape, string color) => new WindowButtons();
    public ICheckbox CreateCheckbox(string shape, string color) => new WindowCheckbox();
}
class MacDialog : Dialog
{
    public IButton CreateButton(string shape, string color) => new MacButtons();
    public ICheckbox CreateCheckbox(string shape, string color) => new MacCheckbox();
}

class Client
{
    string GetCurrentOS() => "Window";
    public void Run()
    {
        Dialog dialog = GetCurrentOS() switch
        {
            "Window" => new WindowDialog(),
            "Mac" => new MacDialog(),
            _ => throw new NotImplementedException(),
        };

        InitUI(dialog);
    }

    public void InitUI(Dialog dialog)
    {
        var okRedButton = dialog.CreateButton("Circle", "Red");
        var okGreenCheckbox = dialog.CreateCheckbox("Square", "Green");
        var otherRedButton = dialog.CreateButton("Circle", "Red");
        okRedButton.Render();
        okGreenCheckbox.Render();
        otherRedButton.Render();
    }
}