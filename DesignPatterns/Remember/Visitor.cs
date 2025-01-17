using static System.Console;
namespace DesignPatterns.Remember.Visitor;

interface IVisitor
{
    void Visit(ConcreteElementA elementA);
    void Visit(ConcreteElementB elementB);
}
class ConcreteVisitor : IVisitor
{
    public void Visit(ConcreteElementA elementA) => elementA.FeatureA();
    public void Visit(ConcreteElementB elementB) => elementB.FeatureB();
}

interface IElement
{
    void Accept(IVisitor visitor);
}
class ConcreteElementA : IElement
{
    public void Accept(IVisitor visitor) => visitor.Visit(this);
    public void FeatureA() => WriteLine($"ConcreteElementA do Feature A");
}
class ConcreteElementB : IElement
{
    public void Accept(IVisitor visitor) => visitor.Visit(this);
    public void FeatureB() => WriteLine($"ConcreteElementB do Feature B");
}

class Client
{
    public void Run()
    {
        IVisitor visitor = new ConcreteVisitor();
        List<IElement> element = new List<IElement>
        {
            new ConcreteElementA(),
            new ConcreteElementB()
        };

        element.ForEach(x => x.Accept(visitor));
    }
}