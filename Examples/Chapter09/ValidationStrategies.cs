﻿using LaYumba.Functional;
using static LaYumba.Functional.F;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace Boc.Chapter9;

using static ValidationStrategies;

public static partial class ValidationStrategies
{
    static readonly Validator<int> Success = i => Valid(i);
    static readonly Validator<int> Failure = _ => Error("Invalid");

    public static void Run()
    {
        var validators = List(Success, Failure, Failure);

        //var validationStrategies = FailFast(validators);
        //var result = validationStrategies(1);
        //WriteLine(result);

        //var validationStrategies2 = FailFast2(validators);
        //var result2 = validationStrategies2(1);
        //WriteLine(result2);

        var result3 = HarvestErrors(List(Success, Failure, Failure, Success))(1);
        WriteLine(result3);
    }

    // runs all validators, and fails when the first one fails
    public static Validator<T> FailFast<T>
       (IEnumerable<Validator<T>> validators)
       => t
       => validators.Aggregate(Valid(t)
          , (acc, validator) => acc.Bind(_ => validator(t)));

    public static Validator<T> FailFast2<T>
       (IEnumerable<Validator<T>> validators)
       => t =>
       {
           return validators.Aggregate(Valid(t),
             (acc, validator) =>
             {
                 return acc.Bind(
                   _ =>
                   {
                       WriteLine(validator(t));
                       return validator(t);
                   });
             });
       };

    // runs all validators, accumulating all validation errors (chapter 7)
    public static Validator<T> HarvestErrors<T>
       (IEnumerable<Validator<T>> validators)
       => t =>
    {
        var errors = validators
          .Map(validate => validate(t))
          .Bind(v => v.Match(
             Invalid: errs =>
             {
                 var result = Some(errs);
                 return result;
             },
             Valid: _ => None))
          .ToList();

        return errors.Count == 0
          ? Valid(t)
          : Invalid(errors.Flatten());
    };
}

public static partial class ValidationStrategiesTest
{
    static readonly Validator<int> Success = i => Valid(i);
    static readonly Validator<int> Failure = _ => Error("Invalid");

    public class FailFastTest
    {
        [Test]
        public void WhenAllValidatorsSucceed_ThenSucceed() => Assert.AreEqual(
           actual: FailFast(List(Success, Success))(1),
           expected: Valid(1)
        );

        [Test]
        public void WhenNoValidators_ThenSucceed() => Assert.AreEqual(
           actual: FailFast(List<Validator<int>>())(1),
           expected: Valid(1)
        );

        [Test]
        public void WhenOneValidatorFails_ThenFail() =>
           FailFast(List(Success, Failure))(1).Match(
              Valid: (_) => Assert.Fail(),
              Invalid: (errs) => Assert.AreEqual(1, errs.Count()));

        [Test]
        public void WhenSeveralValidatorsFail_ThenFail() =>
           FailFast(List(Success, Failure, Failure, Success))(1).Match(
              Valid: (_) => Assert.Fail(),
              Invalid: (errs) => Assert.AreEqual(1, errs.Count())); // only the first error is returned
    }

    public class HarvestErrorsTest
    {
        [Test]
        public void WhenAllValidatorsSucceed_ThenSucceed() => Assert.AreEqual(
           actual: HarvestErrors(List(Success, Success))(1),
           expected: Valid(1)
        );

        [Test]
        public void WhenNoValidators_ThenSucceed() => Assert.AreEqual(
           actual: HarvestErrors(List<Validator<int>>())(1),
           expected: Valid(1)
        );

        [Test]
        public void WhenOneValidatorFails_ThenFail() =>
           HarvestErrors(List(Success, Failure))(1).Match(
              Valid: (_) => Assert.Fail(),
              Invalid: (errs) => Assert.AreEqual(1, errs.Count()));

        [Test]
        public void WhenSeveralValidatorsFail_ThenFail() =>
           HarvestErrors(List(Success, Failure, Failure, Success))(1).Match(
              Valid: (_) => Assert.Fail(),
              Invalid: (errs) => Assert.AreEqual(2, errs.Count())); // all errors are returned
    }
}
