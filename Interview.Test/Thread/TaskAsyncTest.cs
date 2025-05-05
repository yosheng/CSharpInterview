namespace Interview.Test.Thread;
using System.Threading; 

public class TaskAsyncTest : XunitContextBase
{
    [Fact]
    public void Test1()
    {
        Action();
    }
    
    public static void Action()
    {
        Console.WriteLine($"主方法已执行，当前主线程Id为：{Thread.CurrentThread.ManagedThreadId}");
        CallMyJobAsync("jack");
        Console.WriteLine($"尾部已执行，当前主线程Id为：{Thread.CurrentThread.ManagedThreadId}");
    }

    static async void CallMyJobAsync(string name)
    {
        Console.WriteLine($"开始执行任务，当前线程Id为：{Thread.CurrentThread.ManagedThreadId}");
        string result = await MyJobAsync(name);
        Console.WriteLine($"异步完成任务，当前线程Id为：{Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine(result);
    }
    
    static Task<string> MyJobAsync(string name)
    {
        return Task.Run<string>(() =>
        {
            Task.Delay(1000).Wait();
            Console.WriteLine($"准备SayHi，当前线程Id为：{Thread.CurrentThread.ManagedThreadId}");
            return SayHi(name);
        });
    }
    
    static string SayHi(string name)
    {
        Task.Delay(2000).Wait();//异步等待2s
        Console.WriteLine($"SayHi执行，当前线程Id为：{Thread.CurrentThread.ManagedThreadId}");
        return $"Hi {name}";
    }

    public TaskAsyncTest(ITestOutputHelper output) : base(output)
    {
    }
}