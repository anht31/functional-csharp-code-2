using System;
using System.Collections.Generic;

using static LaYumba.Functional.F;
using Unit = System.ValueTuple;

using NUnit.Framework;

namespace Examples.Chapter14;

#region Test type variable
public class Child<T>
{
    public T t;
}

public class Parrent<TT>
{
    public Parrent()
    {
        var child = new Child<int>();

        var otherChild = new Child<TT>();
    }
}
#endregion Test type variable

public delegate dynamic Middleware<T>(Func<T, dynamic> cont);

public delegate dynamic Middleware<T, R>(Func<T, R> cont);

public static class Middleware
{
    public static T Run<T>(this Middleware<T> mw)
    {
        T t = default;
        var typeOfT = t.GetType(); 
        WriteLine(typeOfT);
        return mw(t => t);
    }

    public static Middleware<R> Map<T, R>
       (this Middleware<T> mw, Func<T, R> f)
       => Select(mw, f);

    public static Middleware<R> Bind<T, R>
       (this Middleware<T> mw, Func<T, Middleware<R>> f)
       => SelectMany(mw, f);

    public static Middleware<R> Select<T, R>
       (this Middleware<T> mw, Func<T, R> f)
       => cont => mw(t => cont(f(t)));

    public static Middleware<R> SelectMany<T, R>
       (this Middleware<T> mw, Func<T, Middleware<R>> f)
       => cont => mw(t => f(t)(cont));

    public static Middleware<RR> SelectMany<T, R, RR>
       (this Middleware<T> @this, Func<T, Middleware<R>> f, Func<T, R, RR> project)
       => cont => @this(
           t =>
              f(t)(
                  r =>
                      cont(
                          project(t, r))));
}

public class MiddlewreTests
{
    private List<string> sideEffects;

    Middleware<Unit> MwA => handler =>
    {
        sideEffects.Add("Entering A");
        var result = handler(Unit());
        sideEffects.Add("Exiting A");
        return result;
    };

    Middleware<string> MwB => handler =>
    {
        sideEffects.Add("Entering B");
        var result = handler("1");
        sideEffects.Add("Exiting B");
        return result;
    };

    [SetUp]
    public void SetUp()
       => sideEffects = new List<string>();

    [Test]
    public void TestBindWithoutRun()
    {
        var f = (string s) => (dynamic)Int32.Parse(s + "23");
        var result = MwB(f);

        Assert.AreEqual(123, result);
        Assert.AreEqual(new List<string> { "Entering B", "Exiting B" }, sideEffects);
    }

    [Test]
    public void TestPipelineWithOneClause()
    {
        var pipeline = from b in MwB
                       select b + "end";

        Assert.AreEqual("bend", pipeline.Run());
        Assert.AreEqual(new List<string> { "Entering B", "Exiting B" }, sideEffects);
    }

    [Test]
    public void TestPipelineWith2Clauses()
    {
        var pipeline = from _ in MwA
                       from b in MwB
                       select b + "end";

        Assert.AreEqual("bend", pipeline.Run());
        Assert.AreEqual(new List<string> { "Entering A", "Entering B", "Exiting B", "Exiting A" }, sideEffects);
    }

    [Test]
    public void TestPipelineWith3Clauses()
    {
        var pipeline = from _ in MwA
                       from b in MwB
                       from c in MwB
                       select b + c + "end";

        Assert.AreEqual("bbend", pipeline.Run());
        Assert.AreEqual(new List<string> { "Entering A", "Entering B", "Entering B", "Exiting B", "Exiting B", "Exiting A" }, sideEffects);
    }

    [Test]
    public void TestPipelineWith4Clauses()
    {
        var pipeline = from _ in MwA
                       from __ in MwA
                       from b in MwB
                       from c in MwB
                       select b + c + "end";

        Assert.AreEqual("bbend", pipeline.Run());
    }
}
