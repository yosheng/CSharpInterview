namespace Interview.Test;

public class LinqTest : XunitContextBase
{
    public LinqTest(ITestOutputHelper output) : base(output)
    {
    }
    
    [Fact]
    public void LinqTest1()
    {
        Action();
    }

    public static void Action()
    {
        int filterCount = 0;
        var colors = new List<string> { "Red", "Green", "Blue" }
            .Where(x =>
            {
                filterCount++;
                return x.StartsWith("G");
            }).OrderBy(x => x);
        
        Console.WriteLine(filterCount);
        Console.WriteLine(colors.Count());
        Console.WriteLine(filterCount);
    }
}