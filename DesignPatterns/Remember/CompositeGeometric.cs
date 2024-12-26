using System.Xml.Schema;
using static System.Console;
namespace DesignPatterns.Remember.CompositeGeometric;

interface IGraphic
{
    void Move(int x, int y);
    void Draw();
}
class Dot(int x, int y) : IGraphic
{
    protected int xx = x;
    protected int yy = y;
    public virtual void Draw() => WriteLine($"Draw {this.GetType()} position [{xx}, {yy}]");
    public void Move(int xx, int yy)
    {
        this.xx += xx;
        this.yy += yy;
        WriteLine($"Move to [{this.xx}, {this.yy}]");
    }
}
class Circle(int x, int y, int radius) : Dot(x, y)
{
    public override void Draw() => WriteLine($"Draw {this.GetType()} position [{xx}, {yy}] and radius {radius}");
}
class CompoundGraphic : IGraphic
{
    List<IGraphic> _children = new List<IGraphic>();
    public void Add(IGraphic graphic) => _children.Add(graphic);
    public void Remove(IGraphic graphic) => _children.Remove(graphic);
    public void Move(int x, int y) => _children.ForEach(item => item.Move(x, y));
    public void Draw() => _children.ForEach(x => x.Draw());
}
class Client()
{
    public void Run()
    {
        var dot = new Dot(0, 0);
        var circle = new Circle(2, 2, 1);
        var compoundGraphic = new CompoundGraphic();
        compoundGraphic.Add(dot);
        compoundGraphic.Add(circle);

        compoundGraphic.Move(10, 10);
        compoundGraphic.Draw();
    }
}