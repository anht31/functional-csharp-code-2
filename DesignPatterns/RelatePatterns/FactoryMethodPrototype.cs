using static System.Console;
namespace DesignPatterns.RelatePatterns.FactoryMethodPrototype;

interface IPrototype
{
    Button Clone();
}
interface IButton
{
    void Render();
}
class Button : IPrototype, IButton
{
    private string _shape;
    public Button(string shape) => this._shape = shape;

    public virtual Button Clone() => new(this);
    private protected Button(Button button) => this._shape = button._shape;
    public virtual void Render() { }
}
class WindowButtons : Button
{
    private string _color;
    public WindowButtons(WindowButtons button) : base(button) 
        => _color = button._color;
    public override WindowButtons Clone() => new WindowButtons(this);

    public WindowButtons(string shape, string color) : base(shape) 
        => _color = color;
    public string Color => _color;
    public override void Render() => WriteLine($"WindowButton {_color} Rendered");
}
class MacButtons : Button
{
    private string _color;
    public MacButtons(MacButtons button) : base(button)
        => this._color = button._color;
    public override MacButtons Clone() => new MacButtons(this);

    public MacButtons(string shape, string color) : base(shape)
        => this._color = color;
    public string Color => _color;
    public override void Render() => WriteLine($"MacButtons {_color} Rendered");
}

abstract class Dialog
{
    private protected Dictionary<string, Button> _items = new Dictionary<string, Button>();
    private protected string GetKey(string os, string shape, string color) => $"{os}-{shape}-{color}";

    public void Render()
    {
        var okRedButton = CreateButton("Circle", "Red");
        var okGreenButton = CreateButton("Square", "Green");
        var otherRedButton = CreateButton("Circle", "Red");
        okRedButton.Render();
        okGreenButton.Render();
        otherRedButton.Render();
        WriteLine($"Total Prototypes: {_items.Count}");
    }
    public abstract IButton CreateButton(string shape, string color);
}
class WindowDialog : Dialog
{
    public override IButton CreateButton(string shape, string color)
    {
        string key = GetKey("Win", shape, color);
        if (_items.TryGetValue(key, out Button? button) && button is WindowButtons windowButton)
            return windowButton.Clone();

        return _items[key] = new WindowButtons(shape, color);
        //button = new WindowButtons(shape, color);
        //_items.Add(key, button);
        //return button;
    }
}
class MacDialog : Dialog
{
    public override IButton CreateButton(string shape, string color)
    {
        string key = GetKey("Mac", shape, color);
        if (_items.TryGetValue(key, out Button? button) && button is MacButtons macButton)
            return macButton.Clone();

        return _items[key] = new MacButtons(shape, color);
        //button = new MacButtons(shape, color);
        //_items.Add(key, button);
        //return button;
    }
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