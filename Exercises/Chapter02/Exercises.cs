using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using static System.Console;

namespace Exercises.Chapter2
{
    static class Exercises
    {
        public static void Run()
        {

            //var range = Enumerable.Range(1, 20);

            //Func<int, bool> IsEven = (int i) => i % 2 == 0;

            //var results = range.Where(IsEven);

            //results.ToList().ForEach(Console.WriteLine);


            //var nums = Enumerable.Range(-2000, 4001).Reverse().ToList();
            //// => [10000, 9999, ... , -9999, -10000]

            //var task1 = () => WriteLine($"{nums.Sum()}");
            //var task2 = () => {
            //    var result = QuickSort(nums);
            //    WriteLine(result.Sum());
            //};

            //Parallel.Invoke(task1, task2);

            var connString = "";
            var result = Using(() => new SqlConnection(connString), conn =>
            {
                conn.Open();
                // Thực hiện một số thao tác với cơ sở dữ liệu...
                return "Success!";
            });
        }

        // 1. Write a function that negates a given predicate: whenvever the given predicate
        // evaluates to `true`, the resulting function evaluates to `false`, and vice versa.
        public static Func<T, bool> Nagative<T>(Func<T, bool> predicate)
              => t => !predicate(t);

        // 2. Write a method that uses quicksort to sort a `List<int>` (return a new list,
        // rather than sorting it in place).
        //public static List<int> QuickSort(List<int> clonelist, int leftIndex, int rightIndex)
        //{
        //    var list = new List<int>(clonelist);
        //    var i = leftIndex;
        //    var j = rightIndex;
        //    var pivot = list[leftIndex];

        //    while (i <= j)
        //    {
        //        while (list[i] < pivot)
        //        {
        //            i++;
        //        }

        //        while (list[j] > pivot)
        //        {
        //            j--;
        //        }
        //        if (i <= j)
        //        {
        //            int temp = list[i];
        //            list[i] = list[j];
        //            list[j] = temp;
        //            i++;
        //            j--;
        //        }
        //    }

        //    if (leftIndex < j)
        //        QuickSort(list, leftIndex, j);
        //    if (i < rightIndex)
        //        QuickSort(list, i, rightIndex);
        //    return list;
        //}

        // 3. Generalize your implementation to take a `List<T>`, and additionally a 
        // `Comparison<T>` delegate.

        // 4. In this chapter, you've seen a `Using` function that takes an `IDisposable`
        // and a function of type `Func<TDisp, R>`. Write an overload of `Using` that
        // takes a `Func<IDisposable>` as first
        // parameter, instead of the `IDisposable`. (This can be used to fix warnings
        // given by some code analysis tools about instantiating an `IDisposable` and
        // not disposing it.)
        public static R Using<TDisp, R>(Func<TDisp> funcDisposable, Func<TDisp, R> func) 
            where TDisp : IDisposable
        {
            using (var disp = funcDisposable()) return func(disp);
        }

    }
}
