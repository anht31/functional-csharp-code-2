using static System.Console;

namespace DesignPatterns.Remember.Prototype;

interface IPrototype
{
    ConcretePrototype Clone();
}
class ConcretePrototype : IPrototype
{
    public string Name { get; set; }
    private string PrivateBaseField { get; set; }
    public ConcretePrototype(string name)
    {
        Name = name;
        PrivateBaseField = $"{Name}+1";
    }
    public ConcretePrototype(ConcretePrototype prototype)
    {
        this.Name = prototype.Name;
        this.PrivateBaseField = prototype.PrivateBaseField;
    }

    public virtual ConcretePrototype Clone() => new ConcretePrototype(this);
}
class SubClassPrototype : ConcretePrototype
{
    private string PrivateSubClassPrototype { get; set; }
    public SubClassPrototype(string name) : base(name)
    {
        PrivateSubClassPrototype = $"{Name}+2";
    }
    public SubClassPrototype(SubClassPrototype prototype) : base(prototype)
    {
        this.PrivateSubClassPrototype = prototype.PrivateSubClassPrototype;
    }
    public override ConcretePrototype Clone() => new SubClassPrototype(this);
}

class App
{   
    public void Run() {
        var concrete = new SubClassPrototype("Foo");
        var copy = concrete.Clone();
        WriteLine(copy);
    }
}
