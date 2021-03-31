using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

//while (true)
//{
//    Thread.Sleep(500);
//    Console.WriteLine("Hello World!");
//}


//string testString = string.Empty;

//if (testString is not null)
//{
//    testString = "我没赋值了哦";
//    //string testDemo2String = testString with;
//}
//else
//{
//    testString = "我赋值了哦";
//}

//Console.WriteLine(testString); 

int a = 1;
int b = 15;
int c = 32;

TestInitClass testClass = new(a,b);
Console.WriteLine(testClass);

Console.WriteLine(TestClass.NumSum(1,2));


Console.WriteLine(TestClass.FromRainbow(MyEnum.Orange));
Console.ReadLine();



public enum MyEnum
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet
}
public record TestInitClass
{
    public int FirstNum { get; init; }
    public int SecondNum { get; init; }

    public TestInitClass(int firstNum, int secondNum) => (FirstNum, SecondNum) = (firstNum, secondNum);

}

public class TestClass
{
    public int FirstNum { get; set; }
    public int SecondNum { get; set; }

    public static Func<int,int,int> NumSum = (int num1, int num2) =>
    {
        return num1 + num2;
    };

    public static MyEnum FromRainbow(MyEnum colorBand) =>
    colorBand switch
    {
        MyEnum.Red => MyEnum.Red,
        _ => MyEnum.Violet
    };
}