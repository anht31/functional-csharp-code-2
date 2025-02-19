using static System.Console;
namespace DesignPatterns.RelatePatterns.FacadeSingletons;

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
    OtherClass _otherClass;
    AdditionalFacade _additionalFacade;
    private Facade(OtherClass otherClass, AdditionalFacade additionalFacade)
    {
        _otherClass = otherClass;
        _additionalFacade = additionalFacade;
    }
    public static Facade GetInstance() => Instance ??= new Facade(new OtherClass(), new AdditionalFacade());
    public void Execute()
    {
        _otherClass.DoSomething();
        _otherClass.DoAnother();
        _additionalFacade.AnotherOperation();
    }
}