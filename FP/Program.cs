using FP;
using LaYumba.Functional;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using static LaYumba.Functional.F;
using static System.Console;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Examples.Chapter16;
using System.CodeDom.Compiler;
using System.Reactive.Disposables;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.Metadata;


public static class ExMethod
{
    public static IDisposable Trace<T> (this IObservable<T> source, string name)
       => source.Subscribe
       (
          onNext: t => WriteLine($"{name} -> {t}"),
          onError: ex => WriteLine($"{name} ERROR: {ex.Message}"),
          onCompleted: () => WriteLine($"{name} END")
       );

    public static (IObservable<R> Completed, IObservable<Exception> Faulted)
        Safely<T, R>(this IObservable<T> ts, Func<T, Task<R>> f)
        => ts
            .SelectMany(t => f(t).Map(
                    Faulted: ex => ex,
                    Completed: r => Exceptional(r)))
            .Partition();

    static (IObservable<T> Successes, IObservable<Exception> Exceptions)
        Partition<T>(this IObservable<Exceptional<T>> excTs)
    {
        bool IsSuccess(Exceptional<T> ex)
            => ex.Match(_ => false, _ => true);

        T ExtractValue(Exceptional<T> ex)
            => ex.Match(_ => default, t => t);

        Exception ExtractException(Exceptional<T> ex)
            => ex.Match(exc => exc, _ => default);

        var (ts, errs) = excTs.Partition(IsSuccess);
        return
        (
            Successes: ts.Select(ExtractValue),
            Exceptions: errs.Select(ExtractException)
        );
    }
}



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

    public static async Task KeyTest()
    {
        // Create IObservable from enter keys
        var keys = new Subject<ConsoleKeyInfo>();

        // Simulate
        var simulatedKeys = new[]
        {
            new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false),
            new ConsoleKeyInfo('b', ConsoleKey.B, false, false, false),
            new ConsoleKeyInfo('c', ConsoleKey.C, false, false, false),
            new ConsoleKeyInfo('k', ConsoleKey.K, false, true, false), // Alt+K
            new ConsoleKeyInfo('b', ConsoleKey.B, false, true, false), // Alt+B
            new ConsoleKeyInfo('d', ConsoleKey.D, false, false, false),
            new ConsoleKeyInfo('e', ConsoleKey.E, false, false, false),
        };

        var halfSec = TimeSpan.FromMilliseconds(500);

        var keysAlt = keys;
        //.Where(key => key.Modifiers.HasFlag(ConsoleModifiers.Alt));

        //var twoKeyCombis =
        //    from first in keysAlt
        //    from second in keysAlt.Take(halfSec).Take(1)
        //    select (First: first, Second: second);

        var twoKeyCombis = keysAlt
            .SelectMany(
                first => keysAlt.Take(halfSec).Take(1),
                (first, second) => (First: first, Second: second)
            );


        var altKB =
            from pair in twoKeyCombis
            where pair.First.Key == ConsoleKey.K
               && pair.Second.Key == ConsoleKey.B
            select Unit();

        // Subscribe to altKB
        using (keys.Select(x => x.KeyChar).Trace("keys"))
        using (twoKeyCombis.Select(x => $"{x.First.KeyChar}-{x.Second.KeyChar}").Trace("\t\ttwoKeyCombis"))
        using (altKB.Subscribe(_ => WriteLine("\t\t\t\t\tAlt+K, Alt+B pressed in 0.5s")))
        {
            foreach (var key in simulatedKeys)
            {
                await Task.Delay(400);
                keys.OnNext(key);
            }
        }
            

        // Terminate
        WriteLine("\n\nProgram completed, press enter to exit...");
        ReadLine();
    }

    public static void Main()
    {
        KeyTest();


        //var inputs = new Subject<string>();

        //var rates =
        //   from pair in inputs
        //   from rate in RatesApi.GetRateAsync(pair)
        //   select rate;

        //var outputs = from r in rates select r.ToString();

        //using (inputs.Trace("inputs"))
        //using (rates.Trace("rates"))
        //using (outputs.Trace("outputs"))
        //    for (string input; (input = ReadLine().ToUpper()) != "Q";)
        //        inputs.OnNext(input);


        //RunMap();
        //RunBind();
        //RunPipeline();

        ReadLine();
    }

    public static string Indent(int step)
    {
        var result = string.Empty;
        for (int i = 0; i < step; i++)
        {
            result += "\t\t";
        }
        return result;
    }



    //record struct Circle(Point Center, float Radius);

    public static void ArbitraryMonads()
    {
        var chars = new[] { 'a', 'b', 'c' };
        var ints = new[] { 2, 3 };

        var result = chars.Bind(c => ints
                                .Bind(i =>
                                {
                                    List<Tuple<char, int>> result = new List<Tuple<char, int>>();
                                    result.Add(new Tuple<char, int>(c, i));
                                    return result;
                                }));

        WriteLine(result);
    }

    public static void TestRightIdentity()
    {
        var m = Some(5);
        var result = m.Bind(Some);
        WriteLine(m);
        WriteLine(result);
    }

    public static void TestLeftIdentity()
    {
        Func<int, Option<int>> f = x => Some(x * x);
        var result = Some(5).Bind(f);
        WriteLine(result);
        WriteLine(f(5));
    }

    public static Func<Option<Option<T>>, T> Greet<T>(Option<Option<T>> i)
    => t
    =>
    {
        WriteLine();
        WriteLine($"i -> {i}");
        WriteLine($"t -> {t}");

        return i.Match(
            None: () => None,
            Some: (item) => item
        ).Match(
            None: () => default,
            Some: (sub) => sub
        );
    };
}
