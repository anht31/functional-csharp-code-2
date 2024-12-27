using System.Drawing;
using static System.Console;
namespace DesignPatterns.Remember.FlyWeightTreeFunctional;

// Flyweight
class TreeType(string name, string color, string texture)
{
    public void Draw(string canvas, int x, int y)
        => WriteLine($"Draw: {canvas} [{x}, {y}] -> {name}{color}{texture}");
}

// Flyweight Factory
class TreeFactory
{
    private static Dictionary<string, TreeType> _treeTypes = new Dictionary<string, TreeType>();
    public static TreeType GetTreeType(string name, string color, string texture)
        => _treeTypes.TryGetValue($"{name}{color}{texture}", out TreeType? treeType)
            ? treeType
            : _treeTypes[$"{name}{color}{texture}"] = new TreeType(name, color, texture);
    public static int Length() => _treeTypes.Count();
}

// Context
class Tree(int x, int y)
{
    TreeType treeType;
    public Tree(int x, int y, string name, string color, string texture) : this(x, y) 
        => treeType = TreeFactory.GetTreeType(name, color, texture);
    public void Draw(string canvas) => treeType.Draw(canvas, x, y);
}

// Client
class Forest
{
    private List<Tree> _trees = new List<Tree>();
    public void PlantTree(int x, int y, string name, string color, string texture) 
        => _trees.Add(new Tree(x, y, name, color, texture));
    public void Draw(string canvas) => _trees.ForEach(x => x.Draw(canvas));
    public int Length() => _trees.Count();
}

class Client
{
    public void Run()
    {
        var forest = new Forest();

        WriteLine("I plant 20 tree");
        Enumerable.Range(1, 20).ToList()
            .ForEach(x => forest.PlantTree(x, x, $"Name-{x}", $"Color-{x}", $"Texture-{x}"));

        WriteLine("I plant another 20 tree");
        Enumerable.Range(10, 20).ToList()
            .ForEach(x => forest.PlantTree(x, x, $"Name-{x}", $"Color-{x}", $"Texture-{x}"));

        WriteLine($"Total tree planted: {forest.Length()}");
        WriteLine($"Total type created: {TreeFactory.Length()}");
    }
}