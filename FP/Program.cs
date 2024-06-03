using LaYumba.Functional;

using static System.Console;
using Name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;

public class GeneralizingPartial
{
    public void Run()
    {
        var greet = (Greeting gr, Name name) => $"{gr}, {name}";

        Name[] names = { "Tritan", "Ivan" };

        var greetInformally = greet.Apply("Hey");
        names.Map(greetInformally).ForEach(WriteLine);
    }
}

public class Program
{
   public static void Main()
   {
      new GeneralizingPartial().Run();
      Console.ReadLine();
   }
}