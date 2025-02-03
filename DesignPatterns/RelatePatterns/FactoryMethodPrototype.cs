using static System.Console;
namespace DesignPatterns.RelatePatterns.FactoryMethodPrototype;

interface IPrototype
{
    string GetColor();
    Button Clone();
}
interface IButton
{
    void Render();
}
abstract class Button : IPrototype, IButton
{
    private string _shape;
    public Button Clone() => new(this);
    public Button(Button button) => this._shape = button._shape;

    public virtual string GetColor() { }
    public virtual void Render();
}
class WindowButtons : Button
{
    private string _color;
    public WindowButtons Clone() => new WindowButtons(this);
    public WindowButtons(string color) => _color = color;
    public WindowButtons(WindowButtons button) => this._color = button._color;
    public string GetColor() => _color;
    public void Render() => WriteLine($"WindowButton {_color} Rendered");
}
class MacButtons(string _color) : Button
{
    public IPrototype Clone()
    {
        throw new NotImplementedException();
    }

    public string GetColor()
    {
        throw new NotImplementedException();
    }

    public void Render() => WriteLine($"MacButtons {_color} Rendered");
}

abstract class Dialog
{
    public void Render()
    {
        var okRedButton = CreateButton("Red");
        var okGreenButton = CreateButton("Green");
        var otherRedButton = CreateButton("Red");
        okRedButton.Render();
        okGreenButton.Render();
        otherRedButton.Render();
    }
    public abstract IButton CreateButton(string color);
}
class WindowDialog : Dialog
{
    public override IButton CreateButton(string color) => new WindowButtons(color);
}
class MacDialog : Dialog
{
    public override IButton CreateButton(string color) => new MacButtons(color);
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

        dialog.Render();
    }
}