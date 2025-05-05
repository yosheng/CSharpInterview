namespace Interview.Test.Thread;

using System.Threading;

public class TaskExceptionTest : XunitContextBase
{
    [Fact]
    public void Test()
    {
        Action();
    }
    
    public static void Action()
    {
        Console.WriteLine($"{DateTime.Now}启动.....{Thread.CurrentThread.ManagedThreadId}");
        HandleError();
        HandleErrorAsync();
        Console.WriteLine($"{DateTime.Now}完成.....{Thread.CurrentThread.ManagedThreadId}");
    }

    static void HandleError()
    {
        try
        {
            ThrowExceptionAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}同步错误:{e.Message}");
        }
    }
    
    static async void HandleErrorAsync()
    {
        try
        {
            await ThrowExceptionAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}异步错误:{e.Message}");
        }
    }

    static async Task ThrowExceptionAsync()
    {
        await Task.Delay(1000);
        throw new Exception("测试异常");
    }
    
    public TaskExceptionTest(ITestOutputHelper output) : base(output)
    {
    }
}