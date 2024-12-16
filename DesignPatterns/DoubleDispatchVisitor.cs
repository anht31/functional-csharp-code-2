using static System.Console;
namespace DoubleDispatchVisitor;

class Shape  {
    public virtual void Draw() => WriteLine("Draw Shape");
    public virtual void Accept(ExporterVisitor visitor) => visitor.Export(this);
}
class Dot : Shape {
    public override void Draw() => WriteLine("Draw Dot");
    public override void Accept(ExporterVisitor visitor) => visitor.Export(this);
}
class Rectangle : Shape {
    public override void Draw() => WriteLine("Draw Rectangle");
    public override void Accept(ExporterVisitor visitor) => visitor.Export(this);
}

class ExporterVisitor
{
    public void Export(Shape shape) => WriteLine("Export shape");
    public void Export(Rectangle rectangle) => WriteLine("Export rectangle");
}

static class ExporterExtensions
{
    public static void Export(this Shape shape) => WriteLine("Export Shape");
    public static void Export(this Rectangle rectangle) => WriteLine("Export Rectangle");
}

class App {
    void DrawShape(Shape shape) => shape.Draw();
    void ExportShape(Shape shape) => shape.Accept(new ExporterVisitor());
    public void Run() {
        WriteLine("ExportShape(new Rectangle())");
        ExportShape(new Rectangle());
        WriteLine("new Exporter().Export(new Rectangle())");
        new ExporterVisitor().Export(new Rectangle());

        Rectangle shape = new Rectangle();
        shape.Export(); // Gọi Export(Shape), không phải Export(Rectangle)
    }
}