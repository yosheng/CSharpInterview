using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Bogus;

namespace Interview.Test.Advance;

public class Booking
{
    public int Id { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime CreateTime { get; set; }
}

public class OptimizeTest : XunitContextBase
{
    public OptimizeTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void RunBenchmark()
    {
        var logger = new AccumulationLogger();

        var config = ManualConfig.Create(DefaultConfig.Instance)
            .AddLogger(logger)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);

        config.AddJob(Job.Default.WithIterationCount(3));

        var summary = BenchmarkRunner.Run<OptimizeBenchmark>(config);

        Output.WriteLine(logger.GetLog());
    }
    
    /// <summary>
    /// 打印月销售额
    /// </summary>
    /// <param name="bookings">假设 bookings 有 50w 的元素，如何优化?</param>
    public static void Action(List<Booking> bookings)
    {
        for (int i = 1; i <= 12; i++)
        {
            decimal totalPrice = bookings
                .Where(x => x.CreateTime.Month == i)
                .Sum(x => x.Price);
            
            Console.WriteLine($"{i}月总销售额：{totalPrice}");
        }
    }
    
    public static void ActionOptimize(List<Booking> bookings)
    {
        var dictionary = bookings
            .GroupBy(b => b.CreateTime.Month)
            .ToDictionary(g => g.Key, g => g.Sum(b => b.Price));
        
        for (int i = 1; i <= 12; i++)
        {
            Console.WriteLine(dictionary.TryGetValue(i, out var totalPrice) ? $"{i}月总销售额：{totalPrice}" : $"{i}月总销售额：0");
        }
    }
    
    public static void ActionParallel(List<Booking> bookings)
    {
        var monthlyTotals = new ConcurrentDictionary<int, decimal>();

        // 并行分组计算
        Parallel.ForEach(
            bookings,
            booking =>
            {
                int month = booking.CreateTime.Month;
                monthlyTotals.AddOrUpdate(month, booking.Price, (_, existing) => existing + booking.Price);
            });

        for (int month = 1; month <= 12; month++)
        {
            decimal totalPrice = monthlyTotals.TryGetValue(month, out var sum) ? sum : 0;
            Console.WriteLine($"{month}月总销售额：totalPrice");
        }
    }
}

[MemoryDiagnoser]
public class OptimizeBenchmark
{
    private readonly List<Booking> _bookings;

    public OptimizeBenchmark()
    {
        var faker = new Faker<Booking>()
            .RuleFor(b => b.Id, f => f.IndexGlobal)
            .RuleFor(b => b.Price, f => f.Finance.Amount(10, 1000))
            .RuleFor(b => b.CreateTime, f => f.Date.Between(new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)));

        _bookings = faker.Generate(500000); 
    }
    
    [Benchmark]
    public void Action() => OptimizeTest.Action(_bookings);
    
    [Benchmark]
    public void ActionOptimize() => OptimizeTest.ActionOptimize(_bookings);
    
    [Benchmark]
    public void ActionParallel() => OptimizeTest.ActionParallel(_bookings);
}