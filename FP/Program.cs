using LaYumba.Functional;

using static System.Console;
using Name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;
using FP;
using static ExMethod;
using System;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Reactive.Subjects;
using System.Reactive.Linq;

public delegate (T Value, int Seed) Generator<T>(int seed);

public delegate dynamic Middleware<T>(Func<T, dynamic> cont);

public static class MiddlewareExtensions
{
    public static Middleware<R> Bind<T, R>(this Middleware<T> mw
        , Func<T, Middleware<R>> f)
        => contR
            => mw(t
                => f(t)(contR));

    public static Middleware<R> Bind02<T, R>(this Middleware<T> mw02
        , Func<T, Middleware<R>> f)
        => contR_db
            => mw02(t
                => f(t)(contR_db));

    public static Middleware<R> Map<T, R>(this Middleware<T> mw
        , Func<T, R> f)
        => contR
            => mw(t => contR(f(t)));


    public static T Run<T>(this Middleware<T> mw)
        => (T)mw(t => t);
}

public static class ExMethod
{
    public static T Run<T>(this Generator<T> gen, int seed)
        => gen(seed).Value;

    public static T Run<T>(this Generator<T> gen)
        => gen(Environment.TickCount).Value;

    public static Generator<int> NextInt = (seed) =>
    {
        seed ^= seed >> 13;
        seed ^= seed << 18;
        int result = seed & 0x7fffffff;
        return (result, result);
    };

    public static Generator<R> Select<T, R>
    (
        this Generator<T> gen,
        Func<T, R> f
    )
    => seed =>
    {
        var (t, newSeed) = gen(seed);
        return (f(t), newSeed);
    };

    public static Generator<RR> SelectMany<T, R, RR>
    (
        this Generator<T> gen,
        Func<T, Generator<R>> bind,
        Func<T, R, RR> project
    )
    => seed0 =>
    {
        var (t, seed1) = gen(seed0);
        var (r, seed2) = bind(t)(seed1);
        var rr = project(t, r);
        return (rr, seed2);
    };

    public static Generator<bool> NextBool =>
        from i in NextInt
        select i % 2 == 0;

    public static Generator<(int, int)> PairOfInts =>
        from a in NextInt
        from b in NextInt
        select (a, b);
}
public class Program
{
    public static void RunMap()
    {
        Middleware<string> StringMiddleware = cont =>
        {
            string value = "Hello, Middleware!";
            return cont(value);
        };

        // Converse function
        Func<string, int> stringLength = s => s.Length;

        // Combine Middleware and Converse Function
        var lengthMiddleware = StringMiddleware.Map(stringLength);


        // Continuation R
        Func<int, dynamic> printLength = length =>
        {
            WriteLine($"Length: {length}");
            return length;
        };

        // Call Middleware with continuationR
        lengthMiddleware(printLength);

        // use Middleware and call Run
        int result = lengthMiddleware.Run();
        WriteLine($"Result: {result}");

        ReadLine();
    }

    public static void RunBind()
    {
        Middleware<string> StringMiddleware = cont =>
        {
            string value = "StringMiddleware";
            WriteLine(value);
            return cont(value);
        };

        Func<string, Middleware<int>> StringLengthFunction = (string s)
            => cont
                =>
                {
                    int length = s.Length;
                    WriteLine($"String Length: {length}");
                    return cont(length);
                };

        Func<int, dynamic> contR = (int i) =>
        {
            WriteLine($"ContinuationR: {i}");
            return i;
        };

        var lengthMiddleWare = StringMiddleware.Bind(StringLengthFunction);

        lengthMiddleWare(contR);
    }

    private static void RunPipeline()
    {
        Middleware<string> SomeSetupMiddleware = next =>
        {
            WriteLine("Step 1: Some Setup");
            var result = next(null);
            WriteLine("Step 1: Some Teardown");
            return result;
        };

        Middleware<string> StopwatchMiddleware = next =>
        {
            WriteLine("Step 2: Start Stopwatch");
            var result = next(null);
            WriteLine("Step 2: Log time taken");
            return result;
        };

        Middleware<string> ConnectionMiddleware = next =>
        {
            WriteLine("Step 3: Start Stopwatch");
            var result = next(null);
            WriteLine("Step 3: Log time taken");
            return result;
        };

        Func<dynamic, dynamic> databaseAccess = data =>
        {
            WriteLine("Accessing database...");
            return "Database result";
        };

        var pipeline = SomeSetupMiddleware
            .Bind(_ => StopwatchMiddleware)
            .Bind02(_ => ConnectionMiddleware);


        pipeline(databaseAccess);
    }

    

    public static void Main()
    {

        IObservable<ConsoleKeyInfo> keys =
            Observable.Return(new ConsoleKeyInfo((char)ConsoleKey.A, ConsoleKey.A, false, false, false));
        var halfSec = TimeSpan.FromMilliseconds(500);

        var keysAlt = keys
           .Where(key => key.Modifiers.HasFlag(ConsoleModifiers.Alt));

        var twoKeyCombis =
           from first in keysAlt                         
           from second in keysAlt.Take(halfSec).Take(1)  
           select(First: first, Second: second);        
 
        var altKB =
           from pair in twoKeyCombis
           where pair.First.Key == ConsoleKey.K
              && pair.Second.Key == ConsoleKey.B
           select F.Unit();



        //RunMap();
        //RunBind();
        //RunPipeline();
        
        //Thread.Sleep(100);
        //WriteLine(NextInt.Run(2));
        //Thread.Sleep(100);
        //WriteLine(NextInt.Run(3));
        //Thread.Sleep(100);
        //WriteLine(NextInt.Run(3));
        //Thread.Sleep(100);
        //WriteLine(NextInt.Run(3));

        //new Program();

        ReadLine();
    }

    public Program()
    {
        // Assert node has been build Hierarchy
        var nodes = new List<Node>();
        var settings = new List<Setting>().OrderBy(x => x.Level);
        foreach (var setting in settings)
        {
            // handle level by setting
            var currentLevelNodes = nodes.Where(x => x.Level == setting.Level);
            foreach (var node in currentLevelNodes)
            {
                if (node == null) continue;
                var amount = node.AccumulateTree(nodes);
            }
        }
    }

    

    //public static decimal AggregateTree<TSource, TAccumulate>(TSource source, TAccumulate seed, 
    //    Func<TAccumulate, TSource, TAccumulate> func) where TSource : Node
    //{
    //    if (source == null)
    //    {
    //        throw new ArgumentNullException("source null");
    //    }

    //    if (func == null)
    //    {
    //        throw new ArgumentNullException("source null");
    //    }




    //    using (IEnumerator<TSource> e = source.GetEnumerator())
    //    {
    //        if (!e.MoveNext())
    //        {
    //            throw NoElementsException("");
    //        }

    //        TSource result = e.Current;
    //        while (e.MoveNext())
    //        {
    //            result = func(result, e.Current);
    //        }

    //        return result;
    //    }
    //}
}

public static class ExtMethod
{
    public static decimal AccumulateTree(this Node node, List<Node> nodes)
    {
        decimal amount = 0m;
        var childs = nodes.Where(x => x.ParrentId == node.Id);
        if (!childs.Any()) return 0;

        amount += node.Amount;
        amount += childs.Sum(x => x.AccumulateTree(nodes));
        return amount;
    }
}

public class Setting()
{
    public int Level { get; set; }
}


public class Node
{
    public int Id { get; set; }

    public int Level { get; set; }

    public decimal Amount { get; set; }

    public int ParrentId { get; set; }

}