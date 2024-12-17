namespace DesignPatterns.Study.Prototype;
using static System.Console;

public class App
{
    public static void Run()
    {
        var ints = Enumerable.Range(1, 3);
        var result = ints.Where(x => x > 10).FirstOrDefault();
        WriteLine(result);
    }
}