using Examples.Chapter8;
using LaYumba.Functional;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using static System.Console;
using static LaYumba.Functional.F;

namespace Exercises.Chapter9;

static class Exercises
{
    public static void Run()
    {
        var remainderByDivisorForTwo = Remainder.ApplyR(3);
        WriteLine(remainderByDivisorForTwo(3));
        WriteLine(remainderByDivisorForTwo(4));

        ReadLine();
    }

    // 1. Partial application with a binary arithmethic function:
    // Write a function `Remainder`, that calculates the remainder of 
    // integer division(and works for negative input values!). 

    // Notice how the expected order of parameters is not the
    // one that is most likely to be required by partial application
    // (you are more likely to partially apply the divisor).
    static Func<int, int, int> Remainder => (int divided, int divisor) => divided % divisor;
    static Func<int, int, int> Remainder2 = (int divided, int divisor) => divided % divisor;
    static int Remainder3(int divisor, int divided) => divided % divisor;


    // Write an `ApplyR` function, that gives the rightmost parameter to
    // a given binary function (try to write it without looking at the implementation for `Apply`).
    // Write the signature of `ApplyR` in arrow notation, both in curried and non-curried form
    // ApplyR: (((T1, T2) -> R), T2) -> T1 -> R
    // ApplyR: (T1 -> T2 -> R) -> T2 -> T1 -> R (curried form)
    static Func<T1, R> ApplyR<T1, T2, R>(this Func<T1, T2, R> func, T2 t2)
        => t1 => func(t1, t2);

    // Use `ApplyR` to create a function that returns the
    // remainder of dividing any number by 5. 
    static Func<int, int> RemainderOfDevidingBy5 => Remainder.ApplyR(5);

    // Write an overload of `ApplyR` that gives the rightmost argument to a ternary function
    static Func<T1, T2, R> ApplyR<T1, T2, T3, R>(this Func<T1, T2, T3, R> func, T3 t3)
        => (t1, t2) => func(t1, t2, t3);

    // 2. Let's move on to ternary functions. Define a class `PhoneNumber` with 3
    // fields: number type(home, mobile, ...), country code('it', 'uk', ...), and number.
    // `CountryCode` should be a custom type with implicit conversion to and from string.
    enum NumberType { Home, Mobile, Office }

    class PhoneNumber
    {
        public NumberType NumberType;
        public CountryCode CountryCode;
        public string Number;

        public PhoneNumber(NumberType type, string code, string number)
        {
            NumberType = type;
            CountryCode = code;
            Number = number;
        }

        
    }

    record CountryCode (string Code)
    {
        public static implicit operator CountryCode(string s) => new(s);
        public static implicit operator string(CountryCode countryCode) => countryCode.Code;
    }

    // Now define a ternary function that creates a new number, given values for these fields.
    // What's the signature of your factory function? 
    // (string, string, string) -> PhoneNumber
    static Func<CountryCode, NumberType, string, PhoneNumber> CreatePhoneNumber 
        = (CountryCode code, NumberType type, string number)
            => new PhoneNumber(type, code, number);

    // Use partial application to create a binary function that creates a UK number, 
    // and then again to create a unary function that creates a UK mobile
    static Func<NumberType, string, PhoneNumber> CreateNumberUK
        => CreatePhoneNumber.Apply((CountryCode)"uk");

    static Func<string, PhoneNumber> CreateMobileUK(string number)
        => CreateNumberUK.Apply(NumberType.Mobile);

    // 3. Functions everywhere. You may still have a feeling that objects are ultimately 
    // more powerful than functions. Surely, a logger object should expose methods 
    // for related operations such as Debug, Info, Error? 
    // To see that this is not necessarily so, challenge yourself to write 
    // a very simple logging mechanism without defining any classes or structs. 
    // You should still be able to inject a Log value into a consumer class/function, 
    // exposing operations like Debug, Info, and Error, like so:

    
    delegate void Log(Level level, string message);

    static Log WriteLog = (level, message) => WriteLine($"{level}: {message}");

    static void Debug(this Log log, string message) => log(Level.Debug, message);
    static void Info(this Log log, string message) => log(Level.Info, message);
    static void Error(this Log log, string message) => log(Level.Error, message);

    static void _Main()
        => ConsumeLog(WriteLog);

    static void ConsumeLog(Log log)
       => log.Info("look! no objects!");

    enum Level { Debug, Info, Error }


    // 5. Implement Map, Where, and Bind for IEnumerable in terms of Aggregate.
    
    // Implement Map for IEnumerable in terms of Aggregate.
    static IEnumerable<R> MapInTermsOfAggregate<T, R>(this IEnumerable<T> source, Func<T, R> f)
        => source.Aggregate(Enumerable.Empty<R>(), (acc, t) => acc.Concat(List(f(t))));

    // Implement Where for IEnumerable in terms of Aggregate.
    static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source.Aggregate(Enumerable.Empty<T>(), (acc, t) => predicate(t) ? acc.Concat(List(t)) : acc);

    // Implement Bind for IEnumerable in terms of Aggregate.
    static IEnumerable<R> Bind<T, R>(this IEnumerable<T> source, Func<T, IEnumerable<R>> f)
            => source.Aggregate(Enumerable.Empty<R>(), (acc, t) => acc.Concat(f(t)));
}
