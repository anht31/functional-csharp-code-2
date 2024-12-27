using static System.Console;

namespace DesignPatterns.Remember.Singleton;

class Singleton
{
    private static readonly object _lock = new object();
    private static Singleton _instance;
    private Singleton()
    {
    }
    public static Singleton GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
        }
        return _instance;
    }
}

class Client
{
    public void Run()
    {
        var singleton = Singleton.GetInstance();
    }
}
