namespace Interview.Test.Thread;

using System.Threading;

public class TaskSemaphoreSlimTest : XunitContextBase
{
    static SemaphoreSlim _semaphore = new SemaphoreSlim(2);
    
    [Fact]
    public void Test()
    {
        Action();
    }
    
    public static void Action()
    {
        Console.WriteLine($"{DateTime.Now}开始.....{Thread.CurrentThread.ManagedThreadId}");
        Task t1 = Task.Run(() => DoWork(1));
        Task t2 = Task.Run(() => DoWork(2));
        Task t3 = Task.Run(() => DoWork(3));
        Task t4 = Task.Run(() => DoWork(4));

        Task.WaitAll(t1, t2, t3, t4);
        Console.WriteLine($"{DateTime.Now}结束.....{Thread.CurrentThread.ManagedThreadId}");
    }

    static void DoWork(int id)
    {
        _semaphore.Wait();

        try
        {
            Console.WriteLine($"{DateTime.Now}启动任务{id}.....{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    public TaskSemaphoreSlimTest(ITestOutputHelper output) : base(output)
    {
    }
}