namespace Interview.Test.Thread;

using System.Threading;

public class TaskParallelTest : XunitContextBase
{
    [Fact]
    public void Test()
    {
        Action();
        ActionLock();
        ActioniInterLock();
    }
    
    public void Action()
    {
        int a = 0;
        Parallel.For(0, 100000, (i) =>
        {
            a++;
        });
        Console.WriteLine($"总数{a}.....{Thread.CurrentThread.ManagedThreadId}");
    }
    
    public void ActionLock()
    {
        var obj = new Object();
        int a = 0;
        Parallel.For(0, 100000, (i) =>
        {
            lock (obj)
            {
                a++;
            }
        });
        Console.WriteLine($"加锁總數{a}.....{Thread.CurrentThread.ManagedThreadId}");
    }

    public void ActioniInterLock()
    {
        int a = 0;
        Parallel.For(0, 100000, (i) =>
        {
            Interlocked.Increment(ref a);
        });
        Console.WriteLine($"InterLocked總數{a}.....{Thread.CurrentThread.ManagedThreadId}");
    }

    public TaskParallelTest(ITestOutputHelper output) : base(output)
    {
    }
}