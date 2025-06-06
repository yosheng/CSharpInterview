﻿namespace Interview.Test.ObjectOriented;

class A
{
    public A()
    {
        PrintFields();
    }

    public virtual void PrintFields()
    {
    }
}

class B : A
{
    int x = 1;
    int y;

    public B()
    {
        y = -1;
    }

    public override void PrintFields()
    {
        Console.WriteLine("x={0},y={1}", x, y);
    }
}

public class ClassVirtualTest : XunitContextBase
{
    [Fact]
    public void Test()
    {
        Action();
    }

    public static void Action()
    {
        var b = new B();
    }
    
    public ClassVirtualTest(ITestOutputHelper output) : base(output)
    {
    }
}