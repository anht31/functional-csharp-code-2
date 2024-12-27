using static System.Console;
namespace DesignPatterns.Remember.Facade;

class OtherClass
{
    public void DoSomething() { }
    public void DoAnother() { }
}

class AdditionalFacade
{
    public void AnotherOperation() { }
}

class Facade
{
    OtherClass otherClass = new OtherClass();
    AdditionalFacade additionalFacade = new AdditionalFacade();
    public void Execute()
    {
        otherClass.DoSomething();
        otherClass.DoAnother();
        additionalFacade.AnotherOperation();
    }
}
