using System.Collections.Concurrent;

namespace Interview.Test.Advance;

public class Caching<T>
{
    private static ConcurrentDictionary<string, object> CacheDict = new();

    public void SetValue(string key, T value)
    {
        CacheDict.AddOrUpdate(key, value, (key, lastValue) =>
        {
            return value;
        });
    }

    public object this[string key]
    {
        get { return CacheDict[key];}
        set { CacheDict[key] = value;}
    }
}

public class StaticDictTest : XunitContextBase
{
    [Fact]
    public void Test1()
    {
        Action();
    }
    
    public static void Action()
    {
        var names = new Caching<string>();
        names.SetValue("John", "John");

        var emails = new Caching<string>();
        emails.SetValue("John", "John@cc.com");
        
        var ages = new Caching<int>();
        ages.SetValue("John", 26);
        
        Console.WriteLine(names["John"]);
        Console.WriteLine(emails["John"]);
        Console.WriteLine(ages["John"]);
    }

    public StaticDictTest(ITestOutputHelper output) : base(output)
    {
    }
}