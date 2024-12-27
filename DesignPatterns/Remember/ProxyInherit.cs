using static System.Console;
namespace DesignPatterns.Remember.ProxyInherit;

class Service
{
    public void Operation() => WriteLine("Service do something");
}
class Proxy : Service
{
    public void Operation()
    {
        if (CheckAccess("abc"))
        {
            base.Operation();
        }
    }
    private bool CheckAccess(string token) => token == "abc";
}
class Client
{
    public void Run()
    {
        var service = new Proxy();
        service.Operation();
    }
}