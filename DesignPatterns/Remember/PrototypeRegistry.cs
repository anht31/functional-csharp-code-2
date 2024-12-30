using static System.Console;

namespace DesignPatterns.Remember.PrototypeRegistry;

abstract class Prototype
{
    private string Color { get; }
    public Prototype(string color)
    {
        this.Color = color;
    }
    public abstract ButtonPrototype Clone();
    public string GetColor() => this.Color;
}

class ButtonPrototype : Prototype
{
    private string PrivateField { get;}
    public ButtonPrototype(string color) : base(color)
    {
        this.PrivateField = $"{color}+1";
    }
    public ButtonPrototype(ButtonPrototype prototype) : base(prototype.GetColor())
    {
        this.PrivateField = prototype.PrivateField;
    }
    public override ButtonPrototype Clone() => new ButtonPrototype(this);
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
        var greenButton = new ButtonPrototype("Green");
        var redButton = new ButtonPrototype("Red");
        var blueButton = new ButtonPrototype("Blue");

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