using System.Reflection;
using System.Text.Json;

namespace Interview.Test;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Teacher
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class ConvertTest
{
    [Fact]
    public void Test1()
    {
        Action();
    }
    
    public static void Action()
    {
        var teacher = new Teacher()
        {
            Name = "John",
            Age = 26
        };

        Student a = Convert<Teacher, Student>(teacher);
        Person b = Convert<Teacher, Person>(teacher);
    }

    public static T2 Convert<T1, T2>(T1 obj)
    {
        throw new NotImplementedException();
    }
    
    public static T2 ConvertByReflection<T1, T2>(T1 obj)
    {   
        if (obj == null) return default;

        // 获取目标类型的空参构造函数
        var targetType = typeof(T2);
        var constructor = targetType.GetConstructor(Type.EmptyTypes);
        if (constructor == null)
            throw new InvalidOperationException($"{targetType.Name} 必须有无参构造函数");

        // 创建目标对象
        var result = (T2)constructor.Invoke(null);

        // 复制同名属性
        var sourceProperties = typeof(T1).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite);

        foreach (var sourceProp in sourceProperties)
        {
            var targetProp = targetProperties.FirstOrDefault(p => p.Name == sourceProp.Name);
            if (targetProp != null)
            {
                var value = sourceProp.GetValue(obj);
                targetProp.SetValue(result, value);
            }
        }

        return result;
    }
    
    public static T2 ConvertBySerialize<T1, T2>(T1 obj)
    { 
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T2>(json);
    }
}