using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using LaYumba.Functional;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using NUnit.Framework.Internal.Execution;
using static System.Console;

namespace Examples.Chapter18;

static class CreatingObservables
{
    public static class Timer
    {

        public static void Run()
        {
            var oneSec = TimeSpan.FromMilliseconds(1000);

            var ticks = Observable.Interval(oneSec);

            ticks.Take(10).Subscribe(Console.WriteLine);

            Task.Delay(12_000).Wait();
        }

        public static void Run02()
        {
            var oneSec = TimeSpan.FromMilliseconds(1000);

            var ticks = Observable.Interval(oneSec);
            var ticksX10 = ticks.Select(x => x * 10);

            ticks.Take(10).Subscribe(Console.WriteLine);

            Task.Delay(12_000).Wait();
        }


        public static void Run03()
        {
            Subject<string> keys = new Subject<string>();

            IObservable<IObservable<string>> keySS = keys.Select(_ => keys);

            using (keySS.Trace("keySS"))
            {
                keys.OnNext("a");
                keys.OnNext("b");
                keys.OnNext("c");
            }
                
        }

        
        // Bài tập FP level 3
        public static void Run04()
        {
            Subject<string> keys = new Subject<string>();
            IObservable<string> takeOneObs = keys.Take(1);

            var TakeFunc = () =>
            {
                takeOneObs = keys.Take(1);
                return takeOneObs;
            };

            var keysMap = from first in keys
                          from second in TakeFunc()
                          select (first, second);

            using (keys.Trace("keys"))
            using (keysMap.Trace("\t\t\tkeysMap"))
            {
                using (takeOneObs.Trace("\ttakeOne"))
                    keys.OnNext("a");
                using (takeOneObs.Trace("\ttakeOne"))
                    keys.OnNext("b");
                using (takeOneObs.Trace("\ttakeOne"))
                    keys.OnNext("c");
                using (takeOneObs.Trace("\ttakeOne"))
                    keys.OnNext("d");
            }

            // Result:
            //keys->a
            //        takeOne->a
            //        takeOne END
            //keys->b
            //                        keysMap-> (a, b)
            //        takeOne->b
            //        takeOne END
            //keys->c
            //                        keysMap-> (b, c)
            //        takeOne->c
            //        takeOne END
            //keys->d
            //                        keysMap-> (c, d)
            //        takeOne->d
            //        takeOne END
        }
    }

    public static class Subjects
    {
        public static void Run()
        {
            WriteLine("Enter some inputs to push them to 'inputs', or 'q' to quit");

            var inputs = new Subject<string>();

            using (inputs.Trace("inputs"))
            {
                for (string input; (input = ReadLine()) != "q";)
                    inputs.OnNext(input);
                inputs.OnCompleted();
            }
        }
    }

    // alternative methods to capture console inputs as a stream

    public static class Create
    {
        public static void Run()
        {
            var inputs = Observable.Create<string>(observer =>
            {
                for (string input; (input = ReadLine()) != "q";)
                    observer.OnNext(input);
                observer.OnCompleted();

                return () => { };
            });

            inputs.Trace("inputs");
        }
    }

    public static class Generate
    {
        public static void Run()
        {
            var inputs = Observable.Generate<string, string>(ReadLine()
               , input => input != "q"
               , _ => ReadLine()
               , input => input);

            inputs.Trace("inputs");
        }
    }
}
