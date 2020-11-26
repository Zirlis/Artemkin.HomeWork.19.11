using System;
using System.Collections.Generic;
using System.IO;

//2.Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
//а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум.
//б) Используйте массив(или список) делегатов, в котором хранятся различные функции.
//в) *Переделайте функцию Load, чтобы она возвращала массив считанных значений. 
//Пусть она возвращает минимум через параметр.

internal delegate double Function(double x);
class Task2
{
    public static double F1(double x)
    {
        return x * x - 50 * x + 10;
    }

    public static double F2(double x)
    {
        return x * x * x;
    }

    public static double F3(double x)
    {
        return x * x;
    }

    private readonly static Function[] funArray = new Function[3] { F1, F2, F3 };

    public static void SaveFunc(string fileName, double a, double b, double h, int numFunction)
    {
        FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs);
        double x = a;
        while (x <= b)
        {
            bw.Write(funArray[numFunction](x));
            x += h;
        }
        bw.Close();
        fs.Close();
    }
    public static List<double> Load(string fileName, out double minValue)
    {
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        BinaryReader bw = new BinaryReader(fs);
        double min = double.MaxValue;
        double d;
        List<double> yArray = new List<double>();
        for (int i = 0; i < fs.Length / sizeof(double); i++)
        {
            // Считываем значение и переходим к следующему
            d = bw.ReadDouble();
            yArray.Add(d);
            if (d < min) min = d;
        }
        bw.Close();
        fs.Close();
        minValue = min;
        return yArray;
    }    
    internal void TaskMain()
    {
        string testStr;
        int intChoice;

        do
        {
            Console.WriteLine("Выберите функцию");
            Console.WriteLine("1) x * x - 50 * x + 10");
            Console.WriteLine("2) x * x * x");
            Console.WriteLine("3) x * x");

            testStr = Console.ReadLine();
        } while (!Int32.TryParse(testStr, out intChoice) || intChoice > 3 || intChoice < 1);
        intChoice--;

        int startX;        
        do
        {
            Console.WriteLine("Введите начало отрезка, в котором вы хотите узнать минимальное значение");
            testStr = Console.ReadLine();

        } while (!Int32.TryParse(testStr, out startX));

        int finishX;
        do
        {
            Console.WriteLine("Введите конец отрезка, в котором вы хотите узнать минимальное значение");
            testStr = Console.ReadLine();

        } while (!Int32.TryParse(testStr, out finishX) || finishX < startX);


        SaveFunc("data.bin", startX, finishX, 0.5 , intChoice);
        List<double> yArray = Load("data.bin", out double minValue);
        Console.WriteLine(minValue);
        Console.ReadKey();
    }    
}
