using System.Collections.Generic;
using static System.Console;
namespace DesignPatterns.Remember.ShapeVisitor;

interface IVisitor
{
    void VisitDot(Dot dot);
    void VisitCircle(Circle circle);
    void VisitRectangle(Rectangle rectangle);
    void VisitCompoundShape(CompoundShape compoundShape);
}
class XMLExportVisitor : IVisitor
{
    public void VisitCircle(Circle circle) => WriteLine("Export Circle");

    public void VisitCompoundShape(CompoundShape compoundShape) => WriteLine("Export CompoundShape");

    public void VisitDot(Dot dot) => WriteLine("Export Dot");

    public void VisitRectangle(Rectangle rectangle) => WriteLine("Export Rectangle");
}

interface IShape
{
    void Move(int x, int y);
    void Draw();
    void Accept(IVisitor visitor);
}
class Dot : IShape
{
    public void Accept(IVisitor visitor) => visitor.VisitDot(this);
    public void Draw() => WriteLine($"{this.GetType().Name} do Draw");
    public void Move(int x, int y) => WriteLine($"{this.GetType().Name} do Move to [${x}, ${y}]");
}
class Circle : IShape
{
    public void Accept(IVisitor visitor) => visitor.VisitCircle(this);
    public void Draw() => WriteLine($"{this.GetType().Name} do Draw");
    public void Move(int x, int y) => WriteLine($"{this.GetType().Name} do Move to [${x}, ${y}]");
}
class Rectangle : IShape
{
    public void Accept(IVisitor visitor) => visitor.VisitRectangle(this);
    public void Draw() => WriteLine($"{this.GetType().Name} do Draw");
    public void Move(int x, int y) => WriteLine($"{this.GetType().Name} do Move to [${x}, ${y}]");
}
class CompoundShape : IShape
{
    public void Accept(IVisitor visitor) => visitor.VisitCompoundShape(this);
    public void Draw() => WriteLine($"{this.GetType().Name} do Draw");
    public void Move(int x, int y) => WriteLine($"{this.GetType().Name} do Move to [${x}, ${y}]");
}

class Client
{
    public void Run()
    {
        Export();
    }

    private void Export()
    {
        var allShapes = new List<IShape>
        {
            new Dot(),
            new Circle(),
            new Rectangle(),
            new CompoundShape()
        };

        var exportVisitor = new XMLExportVisitor();

        allShapes.ForEach(x => x.Accept(exportVisitor));
    }
}