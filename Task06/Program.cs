using System;

/*
Источник: https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/operator-overloading

Fraction - упрощенная структура, представляющая рациональное число.
Необходимо перегрузить операции:
+ (бинарный)
- (бинарный)
*
/ (в случае деления на 0, выбрасывать DivideByZeroException)

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки, содержацие числители и знаменатели двух дробей, разделенные /, соответственно.
1/3
1/6
Программа должна вывести на экран сумму, разность, произведение и частное двух дробей, соответственно,
с использованием перегруженных операторов (при необходимости, сокращать дроби):
1/2
1/6
1/18
2

Обратите внимание, если дробь имеет знаменатель 1, то он уничтожается (2/1 => 2). Если дробь в числителе имеет 0, то 
знаменатель также уничтожается (0/3 => 0).
Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

public readonly struct Fraction
{
    private readonly int num;
    private readonly int den;

    public Fraction(int numerator, int denominator)
    {
        num = numerator;
        den = denominator;
    }

    public static Fraction Parse(string input)
    {
        int numer, denom;
        string[] num = input.Split('/');
        if (num[0] == "0")
        { numer = 0; denom = 1; }
        else if (num.Length < 2)
        { numer = int.Parse(num[0]); denom = 1; }
        else
        {
            numer = int.Parse(num[0]);
            denom = int.Parse(num[1]);
        }

        return Reduction(numer, denom);
    }

    public static Fraction operator +(Fraction num1, Fraction num2)
    {
        int numer = num1.num * num2.den + num1.den * num2.num;
        int denom = num1.den * num2.den;
        return Reduction(numer, denom);
    }

    public static Fraction operator -(Fraction num1, Fraction num2)
    {
        int numer = num1.num * num2.den - num1.den * num2.num;
        int denom = num1.den * num2.den;
        return Reduction(numer, denom);
    }

    public static Fraction operator *(Fraction num1, Fraction num2)
    {
        int numer = num1.num * num2.num;
        int denom = num1.den * num2.den;
        return Reduction(numer, denom);
    }

    public static Fraction operator /(Fraction num1, Fraction num2)
    {
        if (num2.num == 0 || num2.den == 0) throw new DivideByZeroException();

        int numer = num1.num * num2.den;
        int denom = num1.den * num2.num;
        return Reduction(numer, denom);
    }

    public static Fraction Reduction(int numer, int denom)
    {
        int numer2 = numer;
        int denom2 = denom;
        while (denom2 != 0)
        {
            int tmp = denom2;
            denom2 = numer2 % denom2;
            numer2 = tmp;
        }

        return new Fraction(numer / numer2, denom / numer2);
    }

    public override string ToString()
    {
        if (den == 1)
            return num.ToString();
        if (den < 0)
            return "-" + num + "/" + (-den);
        return num + "/" + den;
    }
}

public static class OperatorOverloading
{
    public static void Main()
    {
        try
        {
            Fraction a = Fraction.Parse(Console.ReadLine());
            Fraction b = Fraction.Parse(Console.ReadLine());

            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("error");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("zero");
        }
    }
}
