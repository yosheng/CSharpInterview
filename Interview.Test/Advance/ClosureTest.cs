namespace Interview.Test.Advance;

public class ClosureTest : XunitContextBase
{
    public static void Action()
    {
        var person = new Person()
        {
            Name = "John",
            Age = 1
        };

        var actions = new List<Action>();

        for (int i = 0; i < 3; i++)
        {
            actions.Add(() =>
            {
                person.Age = person.Age * i;
                Console.WriteLine($"age: {person.Age}");
            });
        }
        
        foreach (var action in actions)
        {
            action.Invoke();
        }
    }
    
    [Fact]
    public void Test()
    {
        Action();
    }
    
    public ClosureTest(ITestOutputHelper output) : base(output)
    {
    }
}