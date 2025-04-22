

public class Program
{
    static async Task Demo()
    {
        Console.WriteLine("A1");          // trước await #1
        await Task.Delay(1000);           // await #1

        Console.WriteLine("A2");          // giữa 2 await
        await Task.Delay(500);            // await #2

        Console.WriteLine("A3");          // sau await #2
    }

    static async Task Main()
    {
        var t = Demo();              // call Demo -> in "1. enter" rồi pop frame
        Console.WriteLine("3. back in Main");
        await t;                     // chờ continuation chạy xong: "2. after await"
    }
}