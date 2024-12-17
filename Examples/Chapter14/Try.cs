using System;
using System.Text.Json;

using LaYumba.Functional;

using NUnit.Framework;

namespace Examples.Chapter14
{
   public class TryTests
   {
        public void RunTest()
        {
            var tryUrl = CreateUri("http://github.com/2");
            var result = tryUrl.Run();
            WriteLine(result);
        }

      Exceptional<Uri> Boilerplate_CreateUri(string uri)
      {
         try { return new Uri(uri); }
         catch (Exception ex) { return ex; }
      }

      record Website(string Name, string Uri);

      public static void Run()
      {
         var json = @"{""Name"":""Github"",
            ""Uri"":""http://github.com""}";
         Console.WriteLine(ExtractUri_Simple(json));
      }

      public static Uri ExtractUri_Simple(string json)
      {
         var deserialized = JsonSerializer.Deserialize<Website>(json);
         return new Uri(deserialized.Uri);
      }

      Try<Uri> ExtractUri_Ugly(string json)
         => Parse<Website>(json)
            .Bind(website => CreateUri(website.Uri));

      Func<string, Try<Uri>> CreateUri = uri => () => new Uri(uri);
        Try<Uri> CreateUriFunc(string uri) => () => new Uri(uri);

      Try<T> Parse<T>(string s) => () => JsonSerializer.Deserialize<T>(s);

      Try<Uri> ExtractUri(string json) =>
         from website in Parse<Website>(json)
         from uri in CreateUri(website.Uri)
         select uri;

      [TestCase(@"{""Name"":""Github"", ""Uri"":""http://github.com""}"
         , ExpectedResult = "Ok")]
      [TestCase(@"{""Name"":""Github"", ""Uri"":""rubbish""}"
         , ExpectedResult = "Invalid URI")]
      [TestCase("{}"
         , ExpectedResult = "Value cannot be null")]
      [TestCase("blah!"
         , ExpectedResult = "'b' is an invalid start of a value")]
      public string SuccessfulTry(string json)
         => ExtractUri(json)
            .Run()
            .Match
            (
               ex => ex.Message.Split(new char[] { '.', ':' })[0],
               _ => "Ok"
            );
   }
}
