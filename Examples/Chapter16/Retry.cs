using System;
using System.Threading.Tasks;
using LaYumba.Functional;

namespace Examples.Chapter16
{
   public static class RetryHelper
   {
      public enum RetryStrategy { Exponential, Fixed };

      public static Task<T> _Retry<T>
         (int retries, int delayMillis, Func<Task<T>> start)
         => retries == 0
            ? start()
            : start().OrElse(() =>
               from _ in Task.Delay(delayMillis)
               from t in Retry(retries - 1, delayMillis * 2, start)
               select t);

      public static async Task<T> Retry<T>
         (int retries, int delayMillis, Func<Task<T>> start)
         => retries == 0
            ? await start()
            : await start().OrElse(async () =>
            {
               await Task.Delay(delayMillis);
               return await Retry(retries - 1, delayMillis * 2, start);
            });
      
      public static void _main()
      {
         var result = Retry(10, 100, () => FxApi.GetRate("GBPUSD"));
      }
   }
}
