using System;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using static System.Console;
using NUnit.Framework.Constraints;
using Unit = System.ValueTuple;
using System.Collections.Generic;
using System.Linq;
using static LaYumba.Functional.Either;

namespace Exercises.Chapter10;

public static class Exercises
{
    public static void Run()
    {
        //TestApply();
        //Composition();
        //Interchange();
        TestExceptional();
    }

    // 0.3 Interchange
    public static void Interchange()
    {
        // Hàm trong ngữ cảnh Option
        var u = Some<Func<int, int>>(x => x + 2);  // Hàm cộng 2
        var y = 5;                                 // Giá trị y thuần túy

        // Bên trái: u <*> pure y
        var leftSide = u.Apply(Some(y));

        // Bên phải: pure ($ y) <*> u
        var rightSide = Some<Func<Func<int, int>, int>>(f => f(y)).Apply(u);
    }

    // 0.4. Composition
    public static void Composition()
    {
        // Các hàm trong ngữ cảnh Option
        var u = Some<Func<int, int>>(x => x + 1);
        var v = Some<Func<int, int>>(x => x * 2);
        var w = Some(1);

        // Bên trái: pure (.) <*> u <*> v <*> w
        var leftSide = Some<Func<Func<int, int>, Func<Func<int, int>, Func<int, int>>>>
                           (f => g => x => f(g(x))) // Hàm kết hợp chuẩn của Composition Law
                               .Apply(u).Apply(v).Apply(w);

        // Bên phải: u <*> (v <*> w)
        var rightSide = u.Apply(v.Apply(w));

        Console.WriteLine(leftSide); // In ra kết quả của leftSide
        Console.WriteLine(rightSide); // In ra kết quả của rightSide

        Console.ReadLine();
    }

    public static void TestApply()
    {
        var u = Some<Func<int, int>>(x => x + 1); // Hàm cộng 1
        var v = Some<Func<int, string>>(x => $"Print: {x}"); // Hàm Print
        var leftSide = Some<Func<Func<int, int>, Func<Func<int, string>, Func<int, string>>>>(f => g => x => g(f(x)));
        WriteLine(leftSide.Apply(u).Apply(v).Apply(1));
    }


    public static void TestExceptional()
    {
        WriteLine("Test Exceptional");
        var f = Exceptional((int i) => i + 1);
        var t = Exceptional(2);
        WriteLine(f.Apply(t));
        ReadLine();
    }


    // 1. Implement `Apply` for `Either` and `Exceptional`.
    // Apply: Either<L, f> -> Either<L, T> -> Either<L, R>
    static Either<LL, RR> Apply<L, R, LL, RR>(
        this Either<LL, Func<R, RR>> valF,
        Either<LL, R> valR
    ) where LL : IEnumerable<L>
        => valF.Match(
            errF => valR.Match<Either<LL, RR>>(
                errR => Left((LL)errF.Concat(errR)),
                r => Left(errF)
            ),
            f => valR.Match<Either<LL, RR>>(
                errR => Left(errR),
                r => Right(f(r))
            )
        );

    // Apply: Exceptional<F> -> Exceptional<T> -> Exceptional<R>
    // f(t) working by -> public static implicit operator Exceptional<T>(T t) => new (t);
    static Exceptional<R> Apply<T, R>(this Exceptional<Func<T, R>> valF, Exceptional<T> valT)
        => valF.Match(
            Exception: ex => valT.Match(
                Exception: exT => new Exception(ex.Message, exT),
                Success: t => ex
            ),
            Success: f => valT.Match<Exceptional<R>>(
                Exception: ex => ex,
                Success: t => f(t) // or F.Exceptional(func(t)))
            )
        );



    // see LaYumba.Functional/Either.cs and LaYumba.Functional/Exceptional.cs

    // 2. Implement the query pattern for `Either` and `Exceptional`. Try to
    // write down the signatures for `Select` and `SelectMany` without
    // looking at any examples. For the implementation, just follow the
    // types--if it type checks, it’s probably right!

    // see LaYumba.Functional/Either.cs and LaYumba.Functional/Exceptional.cs

    // SelectMany: Either<L, T> -> (T -> Either<L, R>) -> ((T, R) -> RR)
    // Select: Either<L, R> -> (R -> Either<L, RR>) -> Either<L, RR>
    static Either<L, RR> SelectMany<L, T, R, RR>(
        this Either<L, T> @this,
        Func<T, Either<L, R>> bind,
        Func<T, R, RR> project)
        => @this.Match(
            Left: l => Left(l),
            Right: t => bind(t).Match<Either<L, RR>>(
                Left: ll => Left(ll),
                Right: r => Right(project(t, r))
            )
        );

    // SelectMany: Exceptional<T> -> (T -> Exceptional<R>) -> ((T, R) -> RR)
    // Select: Exceptional<T> -> (T -> Exceptional<R>) -> Exceptional<R>
    static Exceptional<RR> SelectMany<T, R, RR>(
        this Exceptional<T> @this,
        Func<T, Exceptional<R>> bind,
        Func<T, R, RR> project)
        => @this.Match(
            Exception: ex => ex,
            Success: t => bind(t).Match(
                Exception: ex => ex,
                Success: r => Exceptional(project(t, r))
            )
        );


    // 3. Come up with a scenario in which various `Either`-returning
    // operations are chained with `Bind`. (If you’re short of ideas, you can
    // use the favorite-dish example from Examples/Chapter08/CookFavouriteFood.)
    // Rewrite the code using a LINQ expression.

    class CookFavouriteDish
    {
        Func<Either<Reason, Unit>> WakeUpEarly;
        Func<Unit, Either<Reason, Ingredients>> ShopForIngredients;
        Func<Ingredients, Either<Reason, Food>> CookRecipe;

        Action<Food> EnjoyTogether;
        Action<Reason> ComplainAbout;
        Action OrderPizza;

        void Start()
        {
            WakeUpEarly()
               .Bind(ShopForIngredients)
               .Bind(CookRecipe)
               .Match(
                  Right: dish => EnjoyTogether(dish),
                  Left: reason =>
                  {
                      ComplainAbout(reason);
                      OrderPizza();
                  });
        }

        void FavoriteDish()
        {
            _ = from _ in WakeUpEarly()
                from ingre in ShopForIngredients(_)
                select CookRecipe(ingre).Match
                (
                    Left: reason =>
                    {
                        ComplainAbout(reason);
                        OrderPizza();
                    },
                    Right: food => EnjoyTogether(food)
                );

            // Alternate way
            var result = from _ in WakeUpEarly()
                         from ingre in ShopForIngredients(_)
                         from either in CookRecipe(ingre)
                         select either;

            result.Match(
                Left: reason =>
                {
                    ComplainAbout(reason);
                    OrderPizza();
                },
                Right: food => EnjoyTogether(food)
            );
}
    }

    class Reason { }
    class Ingredients { }
    class Food { }
}
