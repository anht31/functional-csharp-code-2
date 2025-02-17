using static System.Console;
namespace DesignPatterns.RelatePatterns.SingletonsFacade;

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
    private static Facade Instance;
    OtherClass otherClass;
    AdditionalFacade additionalFacade;
    private Facade()
    {
        otherClass = new OtherClass();
        additionalFacade = new AdditionalFacade();
    }
    public static Facade GetInstance() => Instance ??= new Facade();
    public void Execute()
    {
        otherClass.DoSomething();
        otherClass.DoAnother();
        additionalFacade.AnotherOperation();
    }
}

class Client
{
    public void Run() => Facade.GetInstance().Execute();
}