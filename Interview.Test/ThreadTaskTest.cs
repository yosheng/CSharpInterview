namespace Interview.Test;

public class ThreadTaskTest : XunitContextBase
{
    public ThreadTaskTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Test1()
    {
        await Action();
    }

    public void DoSomething(object i)
    {
        Thread.Sleep(5000);
    }

    public async Task DoSomethingAsync(object i)
    {
        await Task.Delay(5000).ConfigureAwait(false);
    }

    public async Task Action()
    {
        List<int> numbers = Enumerable.Range(1, 10).ToList();
        Console.WriteLine($"初始化-{DateTime.Now}");
        //假设打印的时间是2024-05-0100：00:00
        //执行方式1
        var tasks = numbers.Select(x => DoSomethingAsync(x));
        await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);
        Console.WriteLine($"执行方式1-时间大约是？{DateTime.Now}");
        //执行方式2
        Parallel.ForEach(numbers, x => DoSomething(x));
        Console.WriteLine($"执行方式2-时间大约是？{DateTime.Now}");
        //执行方式3
        numbers.ForEach(x => new Thread(DoSomething).Start());
        Console.WriteLine($"执行方式3-时间大约是？{DateTime.Now}");
    }
}