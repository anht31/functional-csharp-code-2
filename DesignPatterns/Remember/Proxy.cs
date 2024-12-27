using static System.Console;
namespace DesignPatterns.Remember.Proxy;

interface ServiceInterface
{
    void Operation();
}
class Service : ServiceInterface
{
    public void Operation() => WriteLine("Service do somthing");
}
class Proxy : ServiceInterface
{
    private Service _realService;
    public Proxy(Service service)
    {
        _realService = service;
    }
    public void Operation()
    {
        if (CheckAccess("abc"))
        {
            _realService.Operation();
        }
    }
    private bool CheckAccess(string token) => token == "abc";
}
class Client
{
    public void Run()
    {
        ServiceInterface service = new Proxy(new Service());
        service.Operation();
    }
}