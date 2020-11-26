using System;
using System.Collections.Generic;
using System.IO;

//3.Переделать программу «Пример использования коллекций» для решения следующих задач:
//а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
//б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся (частотный массив);
//в) отсортировать список по возрасту студента;
//г) *отсортировать список по курсу и возрасту студента;
//д) разработать единый метод подсчета количества студентов по различным параметрам выбора с помощью делегата и методов предикатов.

class Student
{
    public string lastName;
    public string firstName;
    public string university;
    public string faculty;
    public int course;
    public string department;
    public int group;
    public string city;
    public int age;

    public Student(string firstName, string lastName, string university, string faculty, string department, int course, int age, int group, string city)
    {
        this.lastName = lastName;
        this.firstName = firstName;
        this.university = university;
        this.faculty = faculty;
        this.department = department;
        this.course = course;
        this.age = age;
        this.group = group;
        this.city = city;
    }
}
class ProgramTask3
{
    internal delegate int Sort(Student st1, Student st2);
    private readonly static Sort[] sortBy = new Sort[2] { SortByAge, SortByName };

    static int SortByAge(Student st1, Student st2)
    {
        if (st1.age == st2.age) return 0;
        else if (st1.age > st2.age) return 1;
        else return -1;
    }
    static int SortByName(Student st1, Student st2)          
    {

        return String.Compare(st1.firstName, st2.firstName);          
    }
    internal void TaskMain()
    {
        int bakalavr = 0;
        int magistr = 0;
        int youngStudentInFaculty1 = 0;
        int youngStudentInFaculty2 = 0;
        List<Student> list = new List<Student>();                             
        DateTime dt = DateTime.Now;
        StreamReader sr = new StreamReader("students_6.csv");
        while (!sr.EndOfStream)
        {
            try
            {
                string[] s = sr.ReadLine().Split(';');
                list.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7]), s[8]));
                if (int.Parse(s[5]) < 5) bakalavr++; else magistr++;
                if (s[3] == "faculty1" && int.Parse(s[6]) <= 20 && int.Parse(s[6]) >= 18) youngStudentInFaculty1++;
                else if (s[3] == "faculty2" && int.Parse(s[6]) <= 20 && int.Parse(s[6]) >= 18) youngStudentInFaculty2++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                if (Console.ReadKey().Key == ConsoleKey.Escape) return;
            }
        }
        sr.Close();        
        Console.WriteLine("Всего студентов:" + list.Count);
        Console.WriteLine("Магистров:{0}", magistr);
        Console.WriteLine("Бакалавров:{0}", bakalavr);

        string testStr;
        int choice;
        do
        {
            Console.WriteLine("Выберите способ сортировки");
            Console.WriteLine("1) Оставить без сортировки");
            Console.WriteLine("2) Сортировать по имени");
            Console.WriteLine("3) Сортировать по возросту");
            testStr = Console.ReadLine();
        } while (!Int32.TryParse(testStr, out choice) || choice > 3 || choice < 1);
        choice -= 2;

        list.Sort(new Comparison<Student>(sortBy[choice]));
        foreach (var v in list) Console.WriteLine(v.firstName);
        Console.WriteLine(DateTime.Now - dt);
        Console.ReadKey();
    }
}