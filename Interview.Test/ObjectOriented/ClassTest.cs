namespace Interview.Test.ObjectOriented;

public class ParentClass
{
    public ParentClass()
    {
        Console.WriteLine("父");
    }
    
    protected static int InitializeStatic(int i)
    {
        Console.WriteLine($"父-{i}");
        return i;
    }
}

public class SonClass : ParentClass
{
    public static int SonNum = InitializeStatic(10);
    
    public SonClass()
    {
        Console.WriteLine("子");
    }
}

public class ClassTest : XunitContextBase
{
    [Fact]
    public void Test()
    {
        Action();
    }
    
    public static void Action()
    {
        SonClass son = new SonClass();
        Console.WriteLine(SonClass.SonNum);
        SonClass son2 = new SonClass();
        Console.WriteLine(SonClass.SonNum);
    }
    
    public ClassTest(ITestOutputHelper output) : base(output)
    {
    }
}