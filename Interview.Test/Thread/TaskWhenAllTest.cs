namespace Interview.Test.Thread;

using System.Threading;

public class TaskWhenAllTest : XunitContextBase
{
    [Fact]
    public void Test1()
    {
        Action();
    }
    
    public static void Action()
    {
        Console.WriteLine($"{DateTime.Now}启动.....{Thread.CurrentThread.ManagedThreadId}");
        RunAsync();
        Console.WriteLine($"{DateTime.Now}完成.....{Thread.CurrentThread.ManagedThreadId}");
    }
    
    static async void RunAsync()
    {
        var result1 = GetResult(10);
        var result2 = GetResult(20);
        await Task.WhenAll(result1, result2);
        Console.WriteLine($"{DateTime.Now}计算结果:{result1.Result}和{result2.Result}.....{Thread.CurrentThread.ManagedThreadId}");
    }

    static Task<int> GetResult(int num)
    {
        return Task.Run<int>(() =>
        {
            Task.Delay(1000).Wait();
            Console.WriteLine($"{DateTime.Now}计算完成.....{Thread.CurrentThread.ManagedThreadId}");
            return num + 10;
        });
    }
    
    public TaskWhenAllTest(ITestOutputHelper output) : base(output)
    {
    }
}