using static System.Console;
namespace DesignPatterns.Remember.Builder;
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

    // ConcreateBuilder will return different Car
    public Car GetResult() => car;
}
class ManualBuider : IBuilder
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


class Director
{
    IBuilder _buider;
    public void BulidMin(IBuilder builder)
    {
        _buider = builder;
        _buider.SetSeats();
    }
    public void BulidFull(IBuilder builder)
    {
        _buider = builder;
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
        var manualBuilder = new ManualBuider();

        var director = new Director();
        director.BulidFull(builder);
        director.BulidFull(manualBuilder);

        WriteLine(builder.GetResult().ListParts());
        WriteLine(manualBuilder.GetResult().ListParts());
    }
}

