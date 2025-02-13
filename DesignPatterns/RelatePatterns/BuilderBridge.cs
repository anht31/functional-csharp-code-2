using static System.Console;
namespace DesignPatterns.RelatePatterns.BuilderBridge;

interface IBuilder
{
    void Reset();
    void SetSeats();
    void SetEngine();
    void SetGps();
}
class CarBuilder : IBuilder
{
    Car car = new Car();

    public void Reset() => car = new Car();
    public void SetSeats() => car.Add("Seats");
    public void SetEngine() => car.Add("Engine");
    public void SetGps() => car.Add("GPS");
    public Car GetResult() => car;
}
class ManualBuilder : IBuilder
{
    Manual manual = new Manual();

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


abstract class AbstractDirector
{
    private protected IBuilder _buider;
    protected AbstractDirector(IBuilder buider) => _buider = buider;
    public abstract void BuildMin();
    public abstract void BuildFull();
    public void ConfigVehicle(bool isLuxury)
        => WriteLine(isLuxury ? "Applying luxury configuration..."
                                : "Applying standard configuration...");
}
class StandardCarDirector : AbstractDirector
{
    public StandardCarDirector(IBuilder buider) : base(buider) { }
    public override void BuildMin()
    {
        ConfigVehicle(false);
        _buider.Reset();
        _buider.SetSeats();
    }
    public override void BuildFull()
    {
        ConfigVehicle(false);
        _buider.Reset();
        _buider.SetSeats();
        _buider.SetEngine();
        _buider.SetGps();
    }
}

class App
{
    public void Run()
    {
        var builder = new CarBuilder();
        var manualBuilder = new ManualBuilder();

        var director = new StandardCarDirector(builder);
        var manualDirector = new StandardCarDirector(manualBuilder);
        director.BuildFull();
        manualDirector.BuildFull();

        WriteLine(builder.GetResult().ListParts());
        WriteLine(manualBuilder.GetResult().ListParts());
    }
}