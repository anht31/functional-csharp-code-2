using static System.Console;
namespace DesignPatterns.Remember.Bridge;

abstract class Abstraction
{
    protected IImplementation _implementation;
    protected Abstraction(IImplementation implementation) => _implementation = implementation;
    public void Feature1() => _implementation.Method1();
    public void Feature2() => _implementation.Method2();
}
class RefinedAbstraction : Abstraction
{
    public RefinedAbstraction(IImplementation implementation) : base(implementation) { }
    public void FeatureN() => base._implementation.Method3();
}

interface IImplementation
{
    public void Method1();
    public void Method2();
    public void Method3();

}
class ConcreteImplementations : IImplementation
{
    public void Method1() => WriteLine("Method1 call...");

    public void Method2() => WriteLine("Method2 call...");

    public void Method3() => WriteLine("Method3 call...");
}

class Client
{
    public void Run()
    {
        var implementation = new ConcreteImplementations();
        var abstraction = new RefinedAbstraction(implementation);
        abstraction.Feature1();
    }
}