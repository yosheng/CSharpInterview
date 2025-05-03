namespace Interview.Test.Basic;

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class DataTypeTest : XunitContextBase
{
    
    public DataTypeTest(ITestOutputHelper output) : base(output)
    {
    }
    
    [Fact]
    public void Test1()
    {
        DataTypeTest.Action();
    }
    
    public static void Action()
    {
        int year = 20;
        var user = new User()
        {
            Name = "John",
            Age = 26
        };
        
        UpdateValue(year, user);
        
        Console.WriteLine(year);
        Console.WriteLine(user.Name);
        Console.WriteLine(user.Age);
    }

    public static void UpdateValue(int year, User user)
    {
        year = 24;
        user.Name = "Jane";
        user.Age = 27;
    }
}