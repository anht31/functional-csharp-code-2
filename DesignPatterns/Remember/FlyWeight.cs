using System.Reflection.Metadata.Ecma335;
using static System.Console;
namespace DesignPatterns.Remember.FlyWeight;

class FlyWeight
{
    private string _name;
    private string _color;
    public FlyWeight(string name, string color)
    {
        _name = name;
        _color = color;
    }
}

class Context
{
    private FlyWeight _flyWeight;
    private string _uniqueState;
    public Context(string name, string color, string other)
    {
        this._uniqueState = other;
        this._flyWeight = FlyWeightFactory.GetFlyWeight(name, color);
    }
}

class FlyWeightFactory
{
    private static Dictionary<string, FlyWeight> _intrinsics = new Dictionary<string, FlyWeight>();
    public static FlyWeight GetFlyWeight(string name, string color)
    {
        var key = $"{name.Trim()}-{color.Trim()}";
        if (!_intrinsics.TryGetValue(key, out FlyWeight? flyWeight))
        {
            flyWeight = new FlyWeight(name, color);
            _intrinsics.Add(key, flyWeight);
        }
        return flyWeight;
    }
    public static int Length() => _intrinsics.Count();
}

class Client
{
    public void Run()
    {
        var extrinsicButtonGreeen = new Context("button", "green", "private-1");
        var extrinsicButtonRed = new Context("button", "red", "private-2");
        var otherContextButtonGreeen = new Context("button", "green", "private-3");
        WriteLine($"FlyWeightFactory length: {FlyWeightFactory.Length()}");
    }
}