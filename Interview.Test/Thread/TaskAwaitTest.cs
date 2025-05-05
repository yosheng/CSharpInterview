namespace Interview.Test.Thread;

using System.Threading;
using System.Threading.Tasks;

public class TaskAwaitTest : XunitContextBase
{
    [Fact]
    public void Test1()
    {
        Action();
    }

    public void Action()
    {
        Console.WriteLine($"{DateTime.Now}启动.....{Thread.CurrentThread.ManagedThreadId}");
        GetResultAsync();
        Console.WriteLine($"{DateTime.Now}完成.....{Thread.CurrentThread.ManagedThreadId}");
    }

    async static void GetResultAsync()
    {
        var number1 = await GetResult(10);
        Console.WriteLine($"{DateTime.Now}结果1完成{Thread.CurrentThread.ManagedThreadId}");
        var number2 = GetResult(number1);
        Console.WriteLine($"{DateTime.Now}结果分别为：{number1}和{number2.Result}，{Thread.CurrentThread.ManagedThreadId}");
    }

    static Task<int> GetResult(int number)
    {
        Console.WriteLine($"{DateTime.Now}启动任务.....{Thread.CurrentThread.ManagedThreadId}");
        return Task.Run<int>(() =>
        {
            Task.Delay(5000).Wait();
            Console.WriteLine($"{DateTime.Now}结束任务.....{Thread.CurrentThread.ManagedThreadId}");
            return number + 10;
        });
    }

    public TaskAwaitTest(ITestOutputHelper output) : base(output)
    {
    }
}