using System.Reflection;

namespace Interview.Test.Advance;

public class DelegateTest : XunitContextBase
{
    [Fact]
    public void Test1()
    {
        Exec();
    }


    delegate double MyDelegate(int num);

    public static void Exec()
    {
        var myDelegate = new MyDelegate(Run1);
        myDelegate += Run2;
        Console.WriteLine($"结果: {myDelegate(100)}");
    }

    static double Run1(int num)
    {
        var result = num * 0.1; 
        Console.WriteLine($"Run1-{result}");
        return result;
    }
    
    static double Run2(int num)
    {
        var result = num * 0.2;
        Console.WriteLine($"Run2-{result}");
        return result;
    }

    public DelegateTest(ITestOutputHelper output) : base(output)
    {
    }
}