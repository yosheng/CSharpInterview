namespace Interview.Test.Basic;

public class TryCatchTest : XunitContextBase
{
    [Fact]
    public void Test()
    {
        Action();
    }

    public static void Action()
    {
        Console.WriteLine(CalcResult(0));
        Console.WriteLine(CalcResult(1));
    }
    
    public static int CalcResult(int num)
    {
        try
        {
            num = 10 / num;
            return num;
        }
        catch (Exception e)
        {
            num++;
            Console.WriteLine($"Fn-{num}");
            return num;
        }
        finally
        {
            num++;
            Console.WriteLine($"Fn-{num}");
        }
    }

    public TryCatchTest(ITestOutputHelper output) : base(output)
    {
    }
}