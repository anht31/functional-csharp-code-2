using DesignPatterns.Study.DoubleDispatch;
using static System.Console;
namespace DesignPatterns.RelatePatterns.AbstractFactoryPrototype;

interface IPrototype {
    IPrototype Clone();
}
interface IButton {
    void Render();
}
interface ICheckbox {
    void Render();
}
abstract class Button : IPrototype, IButton {
    private string _shape;
    public abstract IPrototype Clone();
    public abstract void Render();
    protected Button(Button button) => _shape = button._shape;
    protected Button(string shape) => this._shape = shape;
    private protected string Shape => _shape;
}
abstract class Checkbox : IPrototype, ICheckbox {
    private string _shape;
    public abstract IPrototype Clone();
    public abstract void Render();
    protected Checkbox(Checkbox checkbox) => this._shape = checkbox._shape;
    protected Checkbox(string shape) => this._shape = shape;
    private protected string Shape => _shape;
}

class WindowButtons : Button {
    private string _color;
    public override IPrototype Clone() => new WindowButtons(this);
    private WindowButtons(WindowButtons button) : base(button) => this._color = button._color;
    public WindowButtons(string shape, string color) : base(shape) => this._color = color;
    public override void Render() => WriteLine($"WindowButton {base.Shape}-{_color} Rendered");
}
class MacButtons : Button {
    private string _color;
    public override IPrototype Clone() => new MacButtons(this);
    private MacButtons(MacButtons button) : base(button) => this._color = button._color;
    public MacButtons(string shape, string color) : base(shape) => _color = color;
    public override void Render() => WriteLine($"MacButtons {base.Shape}-{_color} Rendered");
}
class WindowCheckbox : Checkbox {
    private string _color;
    public override IPrototype Clone() => new WindowCheckbox(this);
    private WindowCheckbox(WindowCheckbox checkbox) : base(checkbox) => this._color = checkbox._color;
    public WindowCheckbox(string shape, string color) : base(shape) => this._color = color;
    public override void Render() => WriteLine($"WindowCheckbox {base.Shape}-{_color} Rendered");
}
class MacCheckbox : Checkbox {
    private string _color;
    public override IPrototype Clone() => new MacCheckbox(this);
    public MacCheckbox(MacCheckbox checkbox) : base(checkbox) => this._color = checkbox._color;
    public MacCheckbox(string shape, string color) : base(shape) => this._color = color;
    public override void Render() => WriteLine($"MacCheckbox {base.Shape}-{_color} Rendered");
}

interface Dialog
{
    IButton CreateButton(string shape, string color);
    ICheckbox CreateCheckbox(string shape, string color);
    int TotalPrototype();
}
class WindowDialog : Dialog
{
    private Dictionary<string, WindowButtons> _buttons = new();
    private Dictionary<string, WindowCheckbox> _checkboxes = new();
    private string GetKey(string shape, string color) => $"win-{shape}-{color}";
    public IButton CreateButton(string shape, string color)
        => _buttons.TryGetValue(GetKey(shape, color), out WindowButtons? button)
            ? (WindowButtons)button.Clone() : _buttons[GetKey(shape, color)] = new WindowButtons(shape, color);
    public ICheckbox CreateCheckbox(string shape, string color) 
        => _checkboxes.TryGetValue(GetKey(shape, color), out WindowCheckbox? checkbox)
            ? (WindowCheckbox)checkbox.Clone() : _checkboxes[GetKey(shape, color)] = new WindowCheckbox(shape, color);
    public int TotalPrototype() => _buttons.Count + _checkboxes.Count;
}
class MacDialog : Dialog
{
    private Dictionary<string, MacButtons> _buttons = new();
    private Dictionary<string, MacCheckbox> _checkboxes = new();
    private string GetKey(string shape, string color) => $"mac-{shape}-{color}";
    public IButton CreateButton(string shape, string color)
        => _buttons.TryGetValue(GetKey(shape, color), out MacButtons? button)
            ? (MacButtons)button.Clone() : _buttons[GetKey(shape, color)] = new MacButtons(shape, color);
    public ICheckbox CreateCheckbox(string shape, string color)
        => _checkboxes.TryGetValue(GetKey(shape, color), out MacCheckbox? checkbox)
            ? (MacCheckbox)checkbox.Clone() : _checkboxes[GetKey(shape, color)] = new MacCheckbox(shape, color);
    public int TotalPrototype() => _buttons.Count + _checkboxes.Count;
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
        WriteLine($"Total Prototype: {dialog.TotalPrototype()}");
    }
}