using System.Runtime.CompilerServices;
using static System.Console;
namespace DesignPatterns.Remember.TemplateMethod;

abstract class AbstractClass
{
    public void TemplateMethod()
    {
        BaseStep01();
        if (BaseStep02())
            RequiredStep03();
        else
            HookStep04();
    }

    public void BaseStep01() => WriteLine($"Base do step01");
    public bool BaseStep02() => false;
    public abstract void RequiredStep03();
    public virtual void HookStep04() { }
}

class ConcreteClassA : AbstractClass
{
    public override void RequiredStep03() => WriteLine($"A do RequiredStep03");
    public override void HookStep04() => WriteLine($"A do HookStep04");
}
class ConcreteClassB : AbstractClass
{
    public override void RequiredStep03() => WriteLine($"B do RequiredStep03");
    public override void HookStep04() => WriteLine($"B do HookStep04");
}

class Client
{
    public void Run()
    {
        WriteLine("Please enter 'A' or 'B'");

        AbstractClass concrete;
        if (ReadLine() == "A")
            concrete = new ConcreteClassA();
        else
            concrete = new ConcreteClassB();

        concrete.TemplateMethod();

    }
}