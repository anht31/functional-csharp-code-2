using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using static System.Console;
using NUnit.Framework.Internal.Execution;

namespace Exercises.Chapter6Exercises;

public static class ExtMethod
{
    public static ISet<R> Map<T, R>
        (this ISet<T> ts, Func<T, R> f)
    {
        var sets = new HashSet<R>();
        foreach (var t in ts)
            sets.Add(f(t));
        return sets;
        //return ts.Select(f).ToHashSet();
    }

    public static IDictionary<K, R> MapDic<K, T, R>
        (this IDictionary<K, T> ts, Func<T, R> f)
    {
        var rs = new Dictionary<K, R>();
        foreach (var t in ts)
            rs[t.Key] = f(t.Value);
        return rs;
    }

    // (Option<T>, (T -> R)) -> Option<R>
    public static Option<R> Map<T, R>
        (this Option<T> optT, Func<T, R> f)
        => optT.Bind(t => Some(f(t)));

    // (IEnumerable<T>, (T -> R)) -> IEnumerable<R>
    public static IEnumerable<R> Map<T, R>
        (this IEnumerable<T> ts, Func<T, R> f)
    {
        foreach (var t in ts)
            yield return f(t);
    }
        //=> ts.Bind(t => List(f(t)));
}

static class Exercises
{
    public static void Run()
    {
        //Run01();
        Run02();

        ReadLine();
    }


    // 1 Implement Map for ISet<T> and IDictionary<K, T>. (Tip: start by writing down
    // the signature in arrow notation.)
    public static void Run01()
    {
        ISet<int> set = new HashSet<int>();
        set.Add(1);
        set.Add(2);
        //set.Add(2); // Không được thêm, vì phần tử đã tồn tại.
        //Console.WriteLine(set.Count); // Kết quả là 2

        Func<int, int> Double = i => i * 2;

        var result = set.Map(Double);
        foreach (var item in result)
            WriteLine(item);

        IDictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("a", 1);
        dic.Add("b", 2);
        dic.Add("c", 3);

        var resultDic = dic.MapDic(x => x * 2);
    }

    // 2 Implement Map for Option and IEnumerable in terms of Bind and Return.
    public static void Run02()
    {
        var list = Enumerable.Range(1, 3);
        var result = list.Map(x => new List<int> { x * 3 });
        WriteLine(result);
        foreach (var item in result)
            WriteLine(item);
    }

    // 3 Use Bind and an Option-returning Lookup function (such as the one we defined
    // in chapter 3) to implement GetWorkPermit, shown below. 

    // Then enrich the implementation so that `GetWorkPermit`
    // returns `None` if the work permit has expired.
    public static void Run03()
    {

    }

    static Option<WorkPermit> GetWorkPermit(Dictionary<string, Employee> people, string employeeId)
        => people.Lookup(employeeId)
            .Bind(employee => employee.WorkPermit)
            .Where(HasNoExpire);

    // Predicate: WorkPermit -> bool
    static Func<WorkPermit, bool> HasNoExpire = permit => permit.Expiry > DateTime.Now;

    // (Option<WorkPermit>, Predicate<WorkPermit, bool>) -> Option<WorkPermit>
    static Option<WorkPermit> CheckExpiry(this WorkPermit permit)
        => permit.Expiry > DateTime.Now ? Some(permit) : None;

    public static Option<T> Lookup<K, T>(this IDictionary<K, T> dict, K key)
        => dict.TryGetValue(key, out T value) ? Some(value) : None;

    // 4 Use Bind to implement AverageYearsWorkedAtTheCompany, shown below (only
    // employees who have left should be included).

    static double AverageYearsWorkedAtTheCompany(List<Employee> employees)
        => employees.Bind(emp => emp.GetYear())
                .Average();

    // Employe -> Option<double>
    static Option<double> GetYear(this Employee emp)
        => emp.LeftOn.Map(leftOn => DiffYears(emp.JoinedOn, leftOn));

    static double DiffYears(DateTime start, DateTime end)
        => (end - start).TotalDays / 365d;
}

public record Employee
(
   string Id,
   Option<WorkPermit> WorkPermit,

   DateTime JoinedOn,
   Option<DateTime> LeftOn
);

public record WorkPermit
(
   string Number,
   DateTime Expiry
);