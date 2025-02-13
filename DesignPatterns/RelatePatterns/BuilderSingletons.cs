using static System.Console;
namespace DesignPatterns.RelatePatterns.BuilderSingletons;

interface IBuilder
{
    void Reset();
    void SetSeats();
    void SetEngine();
    void SetGps();
}
class CarBuilder : IBuilder
{
    private static CarBuilder? _instance;
    private Car car;
    private CarBuilder() => car = new Car();
    public static CarBuilder GetInstance() => _instance ??= new CarBuilder();
    public void Reset() => car = new Car();
    public void SetSeats() => car.Add("Seats");
    public void SetEngine() => car.Add("Engine");
    public void SetGps() => car.Add("GPS");

    // ConcreateBuilder will return different Car
    public Car GetResult() => car;
}
class ManualBuilder : IBuilder
{
    private static ManualBuilder? _instance;
    private Manual manual;
    private ManualBuilder() => manual = new Manual();
    public static ManualBuilder GetInstance() => _instance ??= new ManualBuilder();
    public void Reset() => manual = new Manual();
    public void SetSeats() => manual.Add("Seats");
    public void SetEngine() => manual.Add("Engine");
    public void SetGps() => manual.Add("GPS");

    public Manual GetResult() => manual;
}

class Car
{
    public List<string> _parts = new List<string>();
    public void Add(string part) => _parts.Add(part);
    public string ListParts() => _parts.Aggregate("Car:", (acc, value) => $"{acc} +{value}");
}
class Manual
{
    public List<string> _parts = new List<string>();
    public void Add(string part) => _parts.Add(part);
    public string ListParts() => _parts.Aggregate("Manual:", (acc, value) => $"{acc} +{value}");
}

class Director
{
    private static IBuilder? _builder;
    private static Director? _instance;
    private Director() { }
    public static Director GetInstance() => _instance ??= new Director();
    public void SetInstance(IBuilder builder) => _builder = builder;
    public void BuildMin()
    {
        if (_builder == null) return;
        _builder.Reset();
        _builder.SetSeats();
    }
    public void BuildFull()
    {
        if (_builder == null) return;
        _builder.Reset();
        _builder.SetSeats();
        _builder.SetEngine();
        _builder.SetGps();
    }
}

class Client
{
    public void Run()
    {
        var builder = ManualBuilder.GetInstance();
        var director = Director.GetInstance();
        director.SetInstance(builder);
        director.BuildFull();

        WriteLine(builder.GetResult().ListParts());
    }
}
