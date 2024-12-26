using System.Reflection.Metadata;
using static System.Console;
namespace DesignPatterns.Remember.ObjectAdapter;

class OtherService
{
    public void Excute(int delayTime) => WriteLine($"Other Service: Do some thing with: {delayTime}");
}

interface IAdapter
{
    void Excute(string delayTime);
}

class AdapterClass : IAdapter
{
    private OtherService _adaptee;
    public AdapterClass(OtherService otherService)
    {
        _adaptee = otherService;
    }

    public void Excute(string delayTime)
    {
        var delay = int.TryParse(delayTime, out int value) ? value : 0;
        _adaptee.Excute(delay);
    }
}

class Client
{
    public void Run()
    {
        var otherService = new OtherService();
        IAdapter adapter = new AdapterClass(otherService);
        adapter.Excute("1000");
    }
}