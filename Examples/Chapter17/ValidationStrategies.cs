using LaYumba.Functional;
using static LaYumba.Functional.F;
using NUnit.Framework;
using System.Linq;

using Boc.Chapter9;

namespace Boc.Chapter17
{
    using static ValidationStrategies;

    public static partial class ValidationStrategies
    {
        // runs all validators, accumulating all validation errors
        public static Validator<T> HarvestErrorsTr<T>
           (params Validator<T>[] validators)
           => t
           =>
           {
               var traverseResult = validators
                .Traverse(validate => validate(t));

               return traverseResult.Map(x =>
               {
                   return t;
               });
           };
    }

    public static partial class ValidationStrategiesTest
    {
        static Validator<int> ShouldBePositive = 
            n => (n > 0) ? Valid(n) : Error("Number should be positive");

        static Validator<int> ShoulBeEven =
            n => (n % 2 == 0) ? Valid(n) : Error("Number should be even");

        static Validator<int> ValidateNumber
            = HarvestErrorsTr(ShouldBePositive, ShoulBeEven);

        //[TestCase(2, ExpectedResult = "Valid(2)")]
        //[TestCase(4, ExpectedResult = "Valid(4)")]
        [TestCase(3, ExpectedResult = "Invalid([Number should be even])")]
        public static string TestValidateNumber(int n)
        {
            var result = ValidateNumber(n);
            return result.ToString();
        }

        static Validator<string> ShouldBeLowerCase
           => s
           => (s == s.ToLower()) ? Valid(s) : Error($"{s} should be lower case");

        static Validator<string> ShouldBeOfLength(int n)
           => s
           => (s.Length == n) ? Valid(s) : Error($"{s} should be of length {n}");

        static Validator<string> ValidateCountryCode
           = HarvestErrorsTr(ShouldBeLowerCase, ShouldBeOfLength(2));

        [TestCase("us", ExpectedResult = "Valid(us)")]
        [TestCase("US", ExpectedResult = "Invalid([US should be lower case])")]
        [TestCase("USA", ExpectedResult = "Invalid([USA should be lower case, USA should be of length 2])")]
        public static string TestCountryCodeValidation(string s)
           => ValidateCountryCode(s).ToString();

        public class HarvestErrors_WithTraverse_Test
        {
            static readonly Validator<int> Success = i => Valid(i);
            static readonly Validator<int> Failure = _ => Error("Invalid");

            [Test]
            public void WhenAllValidatorsSucceed_ThenSucceed() => Assert.AreEqual(
               actual: HarvestErrorsTr(Success, Success)(1),
               expected: Valid(1)
            );

            [Test]
            public void WhenNoValidators_ThenSucceed() => Assert.AreEqual(
               actual: HarvestErrorsTr<int>()(1),
               expected: Valid(1)
            );

            [Test]
            public void WhenOneValidatorFails_ThenFail() =>
               HarvestErrorsTr(Success, Failure)(1).Match(
                  Valid: (_) => Assert.Fail(),
                  Invalid: (errs) => Assert.AreEqual(1, errs.Count()));

            [Test]
            public void WhenSeveralValidatorsFail_ThenFail() =>
               HarvestErrorsTr(Success, Failure, Failure, Success)(1).Match(
                  Valid: (_) => Assert.Fail(),
                  Invalid: (errs) => Assert.AreEqual(2, errs.Count())); // all errors are returned
        }
    }
}
