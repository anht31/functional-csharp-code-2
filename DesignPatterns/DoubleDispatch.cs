using static System.Console;
namespace DoubleDispatch;

class Shape  {
    public virtual void Draw() => WriteLine("Draw Shape");
}
class Dot : Shape {
    public override void Draw() => WriteLine("Draw Dot");
}
class Rectangle : Shape {
    public override void Draw() => WriteLine("Draw Rectangle");
}

class Exporter 
{
    public void Export(Shape shape) => WriteLine("Export shape");
    public void Export(Rectangle rectangle) => WriteLine("Export rectangle");
}

class App {
    void DrawShape(Shape shape) => shape.Draw();
    void ExportShape(Shape shape) => new Exporter().Export(shape);
    public void Run() {
        WriteLine("ExportShape(new Rectangle())");
        ExportShape(new Rectangle());
        WriteLine("new Exporter().Export(new Rectangle())");
        new Exporter().Export(new Rectangle());
    }
}