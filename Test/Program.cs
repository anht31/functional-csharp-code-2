namespace AccessModifier;

file class Foo
{
}

class Client
{
    public void Run()
    {
        var fileClass = new Foo();
    }
}