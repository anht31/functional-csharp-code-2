using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Examples.Chapter5;
using System.Security.Cryptography;

namespace Exercises.Chapter7;

public static class Exercises
{
    // 1. Without looking at any code or documentation (or intllisense), write the function signatures of
    // `OrderByDescending`, `Take` and `Average`, which we used to implement `AverageEarningsOfRichestQuartile`:
    static decimal AverageEarningsOfRichestQuartile(List<Person> population)
       => population
          .OrderByDescending(p => p.Earnings)
          .Take(population.Count / 4)
          .Select(p => p.Earnings)
          .Average();

    // OrderByDescending:
    // (IEnumerable<Person>, (Person -> Decimal) -> IEnumerable<Person>
    // (IEnumerable<T>, (T -> Decimal) -> IEnumerable<T>

    // Take:
    // (IEnumerable<Person>, int) -> IEnumerable<Person>
    // (IEnumerable<T>, int) -> IEnumerable<T>

    // Average:
    // IEnumerable<Decimal> -> decimal
    // IEnumerable<T> -> T


    // 2 Check your answer with the MSDN documentation: https://docs.microsoft.com/
    // en-us/dotnet/api/system.linq.enumerable. How is Average different?
    // IEnumerable<TSource> -> IOrderedEnumerable<TSource>
    // IEnumerable<TSource> -> IEnumerable<TSource>
    // IEnumerable<float> -> float

    // 3 Implement a general purpose Compose function that takes two unary functions
    // and returns the composition of the two.
    //static Func<T, Option<R2>> Compose<T, R1, R2>(this Func<T, R1> f, Func<R1, R2> g)
    //    => (T t) => Some(t).Map(f).Map(g);

    static Func<T, R2> Compose<T, R1, R2>(this Func<T, R1> f, Func<R1, R2> g)
        => (T t) => g(f(t));
}
