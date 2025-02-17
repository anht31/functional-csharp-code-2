using static System.Console;
namespace DesignPatterns.RelatePatterns.PrototypeSingletons;

abstract class Prototype
{
    private string Color { get; set; }
    public Prototype(string color)
    {
        this.Color = color;
    }
    public abstract ButtonPrototype Clone();
    public string GetColor() => this.Color;
    public Prototype SetColor(string color)
    {
        this.Color = color;
        return this;
    }
}

class ButtonPrototype : Prototype
{
    private static ButtonPrototype _button;
    private string _shape;
    private string PrivateField { get; }
    private ButtonPrototype(string shape, string color) : base(color)
    {
        this._shape = shape;
        this.PrivateField = $"{color}+1";
    }
    public ButtonPrototype(ButtonPrototype prototype) : base(prototype.GetColor())
    {
        this._shape = prototype._shape;
        this.PrivateField = prototype.PrivateField;
    }
    public override ButtonPrototype Clone() => new ButtonPrototype(this);
    public static ButtonPrototype GetInstance() => _button ??= new ButtonPrototype("square", "default-color");
    public string PropertyPrivateField => this.PrivateField;
}

class PrototypeRegistry
{
    private List<Prototype> items = new();
    public void AddItem(Prototype prototype) => items.Add(prototype);
    public Prototype GetByColor(string color)
        => items.FirstOrDefault(x => x.GetColor() == color)?.Clone();
}

class Client()
{
    public void Run()
    {
        var buttonPrototype = ButtonPrototype.GetInstance();
        var greenButton = buttonPrototype.Clone().SetColor("green");
        var redButton = buttonPrototype.Clone().SetColor("Red");
        var blueButton = buttonPrototype.Clone().SetColor("Blue");

        WriteLine($"greenButton is ButtonPrototype {greenButton is ButtonPrototype}");

        var prototypeRegistry = new PrototypeRegistry();
        prototypeRegistry.AddItem(greenButton);
        prototypeRegistry.AddItem(redButton);
        prototypeRegistry.AddItem(blueButton);

        var needNewBlueButton = prototypeRegistry.GetByColor("Blue");
        WriteLine($"needNewBlueButton -> color: {needNewBlueButton.GetColor()}");
        WriteLine($"needNewBlueButton -> privateField: " +
            $"{(needNewBlueButton as ButtonPrototype).PropertyPrivateField}");
    }
}
