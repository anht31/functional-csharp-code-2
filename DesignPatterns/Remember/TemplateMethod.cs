using System.Runtime.CompilerServices;
using static System.Console;
namespace DesignPatterns.Remember.TemplateMethod;

abstract class AbstractClass
{
    public void TemplateMethod()
    {
        Step01();
        if (Step02())
            Step03();
        else
            Step04();
    }

    public virtual void Step01() => WriteLine($"Base do step01");
    public virtual bool Step02() => false;
    public virtual void Step03() { }
    public virtual void Step04() { }
}

class ConcreteClassA : AbstractClass
{
    public override void Step03() => WriteLine($"A do Step03");
    public override void Step04() => WriteLine($"A do Step04");
}
class ConcreteClassB : AbstractClass
{
    public override void Step01() => WriteLine($"B do Step01");
    public override bool Step02() => true;
    public override void Step03() => WriteLine($"B do Step03");
    public override void Step04() => WriteLine($"B do Step04");
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