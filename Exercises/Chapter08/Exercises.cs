using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;
using Examples.Chapter5;

namespace Exercises.Chapter8;

static class Exercises
{
    // 1. Write a `ToOption` extension method to convert an `Either` into an
    // `Option`. Then write a `ToEither` method to convert an `Option` into an
    // `Either`, with a suitable parameter that can be invoked to obtain the
    // appropriate `Left` value, if the `Option` is `None`. (Tip: start by writing
    // the function signatures in arrow notation)

    // Either<L, R> -> Option<R>
    static Option<R> ToOption<L, R>(this Either<L, R> either)
        => either.Match(
            l => None,
            r => Some(r)
        );

    // (Option<R>, Func<L>) -> Either<L, T>
    static Either<L, R> ToEither<L, R>(this Option<R> opt, Func<L> func)
        => opt.Match<Either<L, R>>(
            () => func(),
            (value) => value
        );

    // 2. Take a workflow where 2 or more functions that return an `Option`
    // are chained using `Bind`.
    static Option<int> ParseInt(string str)
        => int.TryParse(str, out int value) ? Some(value) : None;

    static Option<Age> ParseAge(int number)
        => Age.Create(number);



    static Option<Age> ReturnAge(string str)
        => ParseInt("35").Bind(ParseAge);

    // Then change the first one of the functions to return an `Either`.
    static Either<string, int> ParseIntEither(this string s)
        => ParseInt(s).ToEither(() => "Fail to convert to Either");

    // This should cause compilation to fail. Since `Either` can be
    // converted into an `Option` as we have done in the previous exercise,
    // write extension overloads for `Bind`, so that
    // functions returning `Either` and `Option` can be chained with `Bind`,
    // yielding an `Option`.
    public static Option<RR> Bind<L, R, RR>(this Either<L, R> either, Func<R, Option<RR>> func)
        => either.Match(
            l => None,
            r => func(r)
        );

    public static Option<RR> Bind<L, R, RR>(this Option<R> opt, Func<R, Either<L, RR>> either)
        => opt.Match(
            () => None,
            (value) => either(value).ToOption()
        );

    static Func<string, Option<Age>> ParseAgeUseBind = s
        => ParseIntEither(s).Bind(ParseAge);

    // 3. Write a function `Safely` of type ((() → R), (Exception → L)) → Either<L, R> that will
    // run the given function in a `try/catch`, returning an appropriately
    // populated `Either`.
    static Either<L, R> Safely<L, R>(Func<R> func, Func<Exception, L> left)
    {
        try { return func(); }
        catch (Exception ex) { return left(ex); }
    }

    // 4. Write a function `Try` of type (() → T) → Exceptional<T> that will
    // run the given function in a `try/catch`, returning an appropriately
    // populated `Exceptional`.
    static Exceptional<T> TryExceptionl<T>(Func<T> func)
    {
        try { return func(); }
        catch (Exception ex) { return ex; }
    }
}
