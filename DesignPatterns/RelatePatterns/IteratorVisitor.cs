using static System.Console;
namespace DesignPatterns.RelatePatterns.IteratorVisitor;

interface IIterator
{
    bool HasMore();
    Node GetNext();
}
class GraphIterator : IIterator
{
    private int _position = -1;
    private IGraphCollection _graphCollection;
    private List<Node> _cache;
    public GraphIterator(IGraphCollection graphCollection) => _graphCollection = graphCollection;
    public Node GetNext() => ((Func<object, Node>)(_ => _cache[++_position]))(LazyInit());
    public bool HasMore() => (LazyInit() != null) && _position < (_cache.Count - 1);
    public object LazyInit() => _cache ??= _graphCollection.GetNodes();
}

interface IElement
{
    void Accept(IVisitor visitor);
}
class Node : IElement {
    public virtual void Draw() => WriteLine("Draw Node");
    public virtual void Accept(IVisitor visitor) => WriteLine("Base Accept Execute...");
}
class Square : Node {
    public override void Draw() => WriteLine("Draw Square");
    public override void Accept(IVisitor visitor) => visitor.Export(this);
}
class Circle : Node {
    public override void Draw() => WriteLine("Draw Circle");
    public override void Accept(IVisitor visitor) => visitor.Export(this);
}
class Star : Node {
    public override void Draw() => WriteLine("Draw Star");
    public override void Accept(IVisitor visitor) => visitor.Export(this);
}

interface IGraphCollection
{
    IIterator CreateIterator();
    List<Node> GetNodes();
}
class GraphCollection : IGraphCollection
{
    List<Node> _items = new List<Node>();
    public IIterator CreateIterator() => new GraphIterator(this);
    public List<Node> GetNodes() => _items;

    public void AddNode(Node node) => _items.Add(node);
}

interface IVisitor
{
    void Export(Square square);
    void Export(Circle circle);
    void Export(Star star);
}
class ExporterVisitor : IVisitor
{
    public void Export(Square square) => WriteLine($"Export {square.GetType().Name}");
    public void Export(Circle circle) => WriteLine($"Export {circle.GetType().Name}");
    public void Export(Star star) => WriteLine($"Export {star.GetType().Name}");
}

class Client
{
    public void Run()
    {
        var graph = new GraphCollection();
        graph.AddNode(new Square());
        graph.AddNode(new Circle());
        graph.AddNode(new Star());


        var iterator = new GraphIterator(graph);
        while (iterator.HasMore())
            iterator.GetNext().Draw();

        WriteLine("\nBegin Export");
        var visitor = new ExporterVisitor();
        var iteratorVisitor = new GraphIterator(graph);
        while (iteratorVisitor.HasMore())
            iteratorVisitor.GetNext().Accept(visitor);
    }
}