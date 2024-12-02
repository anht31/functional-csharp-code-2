using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using LaYumba.Functional;

using static System.Console;
using static LaYumba.Functional.F;
using System.Collections.Immutable;
using Unit = System.ValueTuple;
using Examples.Chapter5;

namespace Exercises.Chapter5Exercises;

public static partial class Enum
{
    public static Option<T> Parse<T>(string name) where T : struct
        => System.Enum.TryParse(name, true, out T dayOfWeek) ? Some(dayOfWeek) : None;

    public static Option<R> Bind<T, R>(
        this Option<T> optT, Func<T, Option<R>> f)
        => optT.Match
        (
            () => None,
            (t) => f(t)
        );

    public static Option<R> Map<T, R>(
        this Option<T> optT, Func<T, R> f)
        => optT.Match
        (
            () => None,
            (t) => Some(f(t))
        );

    public static Option<R> Map2<T, R>(
        this Option<T> optT, Func<T, R> f)
        => optT.Bind(t => Some(f(t)));

}

public static class Exercises
{
    public static void Run()
    {
        Option<string> someInt = Some("5");
        Func<string, Option<int>> func = s => Int.Parse(s);

        var result = someInt.Map2(func);   // Kết quả: None

        WriteLine(result);


        //ParseEnum();
        //Lookup();
    }



    // 1 Write a generic function that takes a string and parses it as a value of an enum. It
    // should be usable as follows:

    // Enum.Parse<DayOfWeek>("Friday") // => Some(DayOfWeek.Friday)
    // Enum.Parse<DayOfWeek>("Freeday") // => None
    public static void ParseEnum()
    {
        WriteLine(Enum.Parse<DayOfWeek>("Friday"));
        WriteLine(Enum.Parse<DayOfWeek>("Freeday"));
    }

    // 2 Write a Lookup function that will take an IEnumerable and a predicate, and
    // return the first element in the IEnumerable that matches the predicate, or None
    // if no matching element is found. Write its signature in arrow notation:

    // bool isOdd(int i) => i % 2 == 1;
    // new List<int>().Lookup(isOdd) // => None
    // new List<int> { 1 }.Lookup(isOdd) // => Some(1)
    private static Option<T> Lookup<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        => list.Any(predicate)
            ? Some(list.FirstOrDefault(predicate))
            : None;


    public static void Lookup()
    {
        bool isOdd(int i) => i % 2 == 1;
        WriteLine(new List<int>().Lookup(isOdd));
        WriteLine(new List<int> { 1 }.Lookup(isOdd));
    }

    // 3 Write a type Email that wraps an underlying string, enforcing that it’s in a valid
    // format. Ensure that you include the following:
    // - A smart constructor
    // - Implicit conversion to string, so that it can easily be used with the typical API
    // for sending emails

    public struct EmailSmartStruct
    {
        private readonly Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        private string Value { get; }
        private EmailSmartStruct(string value)
            => Value = value;

        private bool IsValid(string email)
            => regex.Match(email).Success;

        public Option<EmailSmartStruct> Create(string email)
            => IsValid(email) ? Some(new EmailSmartStruct(email)) : None;

        public static implicit operator string(EmailSmartStruct email)
            => email.Value;
    }

    // 4 Take a look at the extension methods defined on IEnumerable inSystem.LINQ.Enumerable.
    // Which ones could potentially return nothing, or throw some
    // kind of not-found exception, and would therefore be good candidates for
    // returning an Option<T> instead?
}
