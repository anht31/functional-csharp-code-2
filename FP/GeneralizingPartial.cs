using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaYumba.Functional;

using static System.Console;
using static LaYumba.Functional.F;
using Name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;
using Microsoft.VisualBasic;


namespace FP;

//public static class Ext
//{
//    public static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> func, T1 t1)
//        => t2 => func(t1, t2);

//    public static Func<T2, T3, R> Apply<T1, T2, T3, R>(this Func<T1, T2, T3, R> func, T1 t1)
//        => (t2, t3) => func(t1, t2, t3);

//    public static Func<T1, Func<T2, R>> Curry<T1, T2, R>
//        (this Func<T1, T2, R> f)
//        => t1 => t2 => f(t1, t2);
//}


public class GeneralizingPartial_0
{
    public void Run()
    {
        var greet = (Greeting gr, string age, Name name) => $"{gr}, {name}, {age}";

        Name[] names = { "Tritan", "Ivan" };

        var greetInformally = greet.Apply("Hey").Apply("NewAge");

        names.Map(greetInformally).ForEach(x =>
        {
            WriteLine(x);
        });
    }
}


public class GeneralizingPartial_1
{

    public void Run()
    {
        //var greetWith = (Greeting gr) => (Name name) => $"{gr}, {name}";

        Name[] names = { "Tritan", "Ivan" };

        //var greetInformally = greetWith("Hello");

        //names.Map(greetInformally).ForEach(WriteLine);

        var result = Some(9.0).Map(Math.Sqrt);
        WriteLine(result);

        var result2 = Some(9.0).Map<double, double>(Math.Sqrt);
        WriteLine(result2);


        PersonalizedGreeting GreeterMethod(Greeting gr, Name name) // local functions (local methods)
            => $"{gr}, {name}";

        //Func<Name, PersonalizedGreeting> GreetWith(Greeting greeting)
        //    => GreeterMethod.Apply(greeting);
        // This line will not compile

        Func<Name, PersonalizedGreeting> GreetWith_1(Greeting greeting)
            => FuncExt.Apply<Greeting, Name, PersonalizedGreeting>(GreeterMethod, greeting);

        Func<Name, PersonalizedGreeting> GreetWith_2(Greeting greeting)
            => new Func<Greeting, Name, PersonalizedGreeting>(GreeterMethod).Apply(greeting);
    }
}


public record ConnectionString(string Value)
{
    public static implicit operator string(ConnectionString c) => c.Value;
    public static implicit operator ConnectionString(string s) => new (s);
}


public class GeneralizingPartial
{

    public void Run()
    {
        var greet = (Greeting gr, Name name) => $"{gr}, {name}";

        Name[] names = { "Tritan", "Ivan" };

        var greetWith = greet.Curry();
        names.Map(greetWith("Hola")).ForEach(WriteLine);


        ConnectionString connnString = "localhost";
        string value = connnString;
    }
}


public class TypeInference_Delegate
{
    readonly string separator = ", ";

    // 1. field
    readonly Func<Greeting, Name, PersonalizedGreeting> GreeterField
        = (gr, name) => $"{gr}, {name}";

    // 2. property
    Func<Greeting, Name, PersonalizedGreeting> GreeterProperty
        => (gr, name) => $"{gr}{separator}{name}";

    //  3. factory
    Func<Greeting, Name, PersonalizedGreeting> GreeterFactory<T>()
        => (gr, t) => $"{gr}{separator}{t}";
}



