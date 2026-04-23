using System;
using System.Collections.Generic;
using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("ЗАДАНИЕ 1");

        string task1Source = "task1.txt";
        string task1Result = "task1_result.txt";

        Console.Write("Количество чисел: ");
        int count1 = int.Parse(Console.ReadLine());

        Console.Write("Минимум: ");
        int min1 = int.Parse(Console.ReadLine());

        Console.Write("Максимум: ");
        int max1 = int.Parse(Console.ReadLine());

        FileTasksPartOne.GenerateSingleNumberPerLineFile(task1Source, count1, min1, max1);
        FileTasksPartOne.CreateFileWithDecreasedNumbers(task1Source, task1Result);


        Console.WriteLine("ЗАДАНИЕ 2");

        string task2Path = "task2.txt";

        Console.Write("Количество строк: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Чисел в строке: ");
        int perRow = int.Parse(Console.ReadLine());

        Console.Write("Минимум: ");
        int min2 = int.Parse(Console.ReadLine());

        Console.Write("Максимум: ");
        int max2 = int.Parse(Console.ReadLine());

        FileTasksPartOne.GenerateMultipleNumbersPerLineFile(task2Path, rows, perRow, min2, max2);

        int result2 = FileTasksPartOne.FindDifferenceBetweenFirstAndMaximum(task2Path);
        Console.WriteLine("Результат: " + result2 + "\n");


        Console.WriteLine("ЗАДАНИЕ 3");

        string task3Source = "task3.txt";
        string task3Result = "task3_result.txt";

        Console.WriteLine("Нажми Enter когда готов файл task3 будет готов");
        Console.ReadLine();

        FileTasksPartOne.CopyLinesStartingWithLetterB(task3Source, task3Result);

        Console.WriteLine("Готово\n");


        Console.WriteLine("ЗАДАНИЕ 4");

        string task4Path = "task4.bin";

        Console.Write("Количество чисел: ");
        int count4 = int.Parse(Console.ReadLine());

        Console.Write("Минимум: ");
        int min4 = int.Parse(Console.ReadLine());

        Console.Write("Максимум: ");
        int max4 = int.Parse(Console.ReadLine());

        FileTasksPartOne.GenerateBinaryFileWithIntegers(task4Path, count4, min4, max4);

        int result4 = FileTasksPartOne.FindDifferenceBetweenFirstAndMaximumInBinaryFile(task4Path);

        Console.WriteLine("Результат: " + result4 + "\n");


        Console.WriteLine("ЗАДАНИЕ 5");

        string task5Path = "task5.xml";

        FileTasksPartOne.GenerateToyXmlFile(task5Path);

        Console.Write("Введите тип игрушки (например: кукла): ");
        string type = Console.ReadLine();

        Console.Write("Введите возраст: ");
        int age = int.Parse(Console.ReadLine());

        List<Toy> toys = FileTasksPartOne.GetToysByTypeAndAge(task5Path, type, age);

        if (toys.Count == 0)
        {
            Console.WriteLine("Ничего не найдено");
        }
        else
        {
            Console.WriteLine("Результат:");
            for (int i = 0; i < toys.Count; i++)
            {
                Console.WriteLine(toys[i].Type + " " + toys[i].Name + " - " + toys[i].Price);
            }
        }

        Console.WriteLine("\nГотово");


        Console.WriteLine("ЗАДАНИЕ 6");

        List<int> list6 = new List<int>();

        Console.Write("Введите количество элементов: ");
        int n6 = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите элементы:");
        for (int i = 0; i < n6; i++)
        {
            list6.Add(int.Parse(Console.ReadLine()));
        }

        Collections.KeepOnlyFirstOccurrences(list6);

        Console.WriteLine("Результат:");
        for (int i = 0; i < list6.Count; i++)
        {
            Console.Write(list6[i] + " ");
        }

        Console.WriteLine("\n");


        Console.WriteLine("ЗАДАНИЕ 7");

        LinkedList<int> list7 = new LinkedList<int>();

        Console.Write("Введите количество элементов: ");
        int n7 = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите элементы:");
        for (int i = 0; i < n7; i++)
        {
            list7.AddLast(int.Parse(Console.ReadLine()));
        }

        Console.Write("Введите значение E: ");
        int value = int.Parse(Console.ReadLine());

        Collections.SwapNeighborsAroundValue(list7, value);

        Console.WriteLine("Результат:");
        foreach (int x in list7)
        {
            Console.Write(x + " ");
        }

        Console.WriteLine("\n");


        Console.WriteLine("ЗАДАНИЕ 8");

        HashSet<string> allLanguages = new HashSet<string>();
        allLanguages.Add("Английский");
        allLanguages.Add("Немецкий");
        allLanguages.Add("Французский");
        allLanguages.Add("Испанский");

        List<HashSet<string>> workers = new List<HashSet<string>>();

        HashSet<string> w1 = new HashSet<string>();
        w1.Add("Английский");
        w1.Add("Немецкий");

        HashSet<string> w2 = new HashSet<string>();
        w2.Add("Английский");
        w2.Add("Французский");

        HashSet<string> w3 = new HashSet<string>();
        w3.Add("Английский");

        workers.Add(w1);
        workers.Add(w2);
        workers.Add(w3);

        HashSet<string> w4 = new HashSet<string>();

        Console.Write("Сколько языков знает 4-й работник: ");
        int langCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите языки:");
        for (int i = 0; i < langCount; i++)
        {
            string lang = Console.ReadLine();
            w4.Add(lang);
        }

        workers.Add(w4);

        HashSet<string> everyone;
        HashSet<string> atLeastOne;
        HashSet<string> nobody;

        Collections.AnalyzeWorkerLanguages(
            allLanguages,
            workers,
            out everyone,
            out atLeastOne,
            out nobody);

        Console.WriteLine("Знают все:");
        foreach (string s in everyone)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("Знает хотя бы один:");
        foreach (string s in atLeastOne)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("Не знает никто:");
        foreach (string s in nobody)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine();
      
      
        Console.WriteLine("ЗАДАНИЕ 9");

        string task9Path = "task9.txt";

        Console.WriteLine("Нажми Enter когда готов файл task9 будет готов");
        Console.ReadLine();

        HashSet<char> letters = Collections.GetStartingLettersOfWords(task9Path);

        Console.WriteLine("Буквы, с которых начинаются слова:");
        foreach (char c in letters)
        {
            Console.Write(c + " ");
        }

        Console.WriteLine("\n");

        Console.WriteLine("ЗАДАНИЕ 10");

        string task10Path = "task10.txt";

        Console.WriteLine("Нажми Enter когда готов файл task10 будет готов");
        Console.ReadLine();

        string result = Collections.GetBestStudentsFromSchool50(task10Path);

        Console.WriteLine("Результат:");
        Console.WriteLine(result);

        Console.WriteLine("\nГотово");
        Console.ReadKey();
    }
}