namespace MyBack.InProcessMessaging;

public interface FirstVeryVeryVeryVeryVeryVeryVeryVeryLongInterface
{
};

public interface SecondVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongInterface
{
};

public class Foo1 : FirstVeryVeryVeryVeryVeryVeryVeryVeryLongInterface,
    SecondVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongInterface
{
    // Long lines - function parameters and arguments
    public void foo1(
        int parameter1,
        int parameter2,
        int parameter3,
        int parameter4,
        int parameter5,
        int parameter6,
        int parameter7,
        int parameter8)
    {
    }

    // Long lines - binary operands
    public double foo2(int value)
    {
        var result = 9.9999999999
            + 9.999999999999
            - 4.5622365
            + 4.44444444444444
            - (2.2222222222222 * value)
            - (2.22222222 * 1.122555222 * value);

        return result;
    }

    // Long lines - ternary expression
    public int foo3(bool condition, int expression)
    {
        var veryVeryVeryVeryVeryVeryVeryVeryVeryLongCondition = condition;
        var firstLongLongLongExpression = 5;
        var secondVeryVeryVeryVeryVeryVeryVeryLongExpression = expression;

        return veryVeryVeryVeryVeryVeryVeryVeryVeryLongCondition
            ? firstLongLongLongExpression
            : secondVeryVeryVeryVeryVeryVeryVeryLongExpression;
    }

    // Long lines - conditions
    public bool foo4(bool condition1, bool condition2, bool condition3, bool condition4)
    {
        var veryVeryVeryVeryVeryVeryVeryVeryLongCondition1 = condition1;
        var veryVeryVeryVeryVeryVeryVeryVeryLongCondition2 = condition2;

        if (veryVeryVeryVeryVeryVeryVeryVeryLongCondition1
            && condition3
            && veryVeryVeryVeryVeryVeryVeryVeryLongCondition2
            && condition4
           )
        {
        }

        return veryVeryVeryVeryVeryVeryVeryVeryLongCondition1
            && condition3
            && veryVeryVeryVeryVeryVeryVeryVeryLongCondition2
            && condition4;
    }

    // Long lines - method chaining
    public void foo5()
    {
        int[] arr = { 1, 2, 3, 4, 5 };

        arr.Select(veryVeryVeryVeryLongName => veryVeryVeryVeryLongName)
            .Select(veryVeryVeryVeryLongName => veryVeryVeryVeryLongName)
            .Select(veryVeryVeryVeryLongName => veryVeryVeryVeryLongName);
    }
}