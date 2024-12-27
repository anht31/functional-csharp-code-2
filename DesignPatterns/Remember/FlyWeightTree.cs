using System.Drawing;
using static System.Console;
namespace DesignPatterns.Remember.FlyWeightTree;

// Flyweight
class TreeType
{
    private string _name;
    private string _color;
    private string _texture;
    public TreeType(string name, string color, string texture)
    {
        _name = name;
        _color = color;
        _texture = texture;
    }
    public void Draw(string canvas, int x, int y) 
        => WriteLine($"Draw: {canvas} [{x}, {y}] -> {_name}{_color}{_texture}");
}

// Flyweight Factory
class TreeFactory
{
    private static Dictionary<string, TreeType> _treeTypes = new Dictionary<string, TreeType>();
    public static TreeType GetTreeType(string name, string color, string texture)
    {
        string key = $"{name}{color}{texture}";
        if (!_treeTypes.TryGetValue(key, out TreeType? treeType))
        {
            treeType = new TreeType(name, color, texture);
            _treeTypes.Add(key, treeType);
        }
        return treeType;
    }
    public static int Length() => _treeTypes.Count();
}

// Context
class Tree
{
    private int x, y;
    TreeType treeType;
    public Tree(int x, int y, TreeType treeType)
    {
        this.x = x;
        this.y = y;
        this.treeType = treeType;
    }
    public void Draw(string canvas) => treeType.Draw(canvas, x, y);
}

// Client
class Forest
{
    private List<Tree> _trees = new List<Tree>();
    public void PlantTree(int x, int y, string name, string color, string texture)
    {
        var type = TreeFactory.GetTreeType(name, color, texture);
        var tree = new Tree(x, y, type);
        _trees.Add(tree);
    }
    public void Draw(string canvas)
    {
        foreach (var tree in _trees)
            tree.Draw(canvas);
    }
}