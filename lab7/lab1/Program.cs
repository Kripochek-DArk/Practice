using System;
using System.Collections.Generic;
using System.Text;

internal class Program
{
    private static int ReadInt(string message)
    {
        int value;
        bool success;

        do
        {
            Console.Write(message);

            success = int.TryParse(Console.ReadLine(), out value);

            if (!success)
            {
                Console.WriteLine("Ошибка: введите целое число");
            }

        } while (!success);

        return value;
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Files.GenerateSingleNumberPerLineFile(
            "Kirill/gde/proverka.txt", 10, 0, 100);

        Console.WriteLine("ЗАДАНИЕ 1");

        string task1Source = "task1.txt";
        string task1Result = "task1_result.txt";

        int count1 = ReadInt("Количество чисел: ");
        int min1 = ReadInt("Минимум: ");
        int max1 = ReadInt("Максимум: ");

        Files.GenerateSingleNumberPerLineFile(
            task1Source, count1, min1, max1);

        Files.CreateFileWithDecreasedNumbers(
            task1Source, task1Result);

        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 2");

        string task2Path = "task2.txt";

        int rows = ReadInt("Количество строк: ");
        int perRow = ReadInt("Чисел в строке: ");
        int min2 = ReadInt("Минимум: ");
        int max2 = ReadInt("Максимум: ");

        Files.GenerateMultipleNumbersPerLineFile(
            task2Path,
            rows,
            perRow,
            min2,
            max2);

        int result2 =
            Files.FindDifferenceBetweenFirstAndMaximum(task2Path);

        Console.WriteLine("Результат: " + result2);
        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 3");

        string task3Source = "task3.txt";
        string task3Result = "task3_result.txt";

        Console.WriteLine(
            "Нажми Enter когда файл task3.txt будет готов");

        Console.ReadLine();

        Files.CopyLinesStartingWithLetterB(
            task3Source,
            task3Result);

        Console.WriteLine("Готово");
        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 4");

        string task4Path = "task4.bin";

        int count4 = ReadInt("Количество чисел: ");
        int min4 = ReadInt("Минимум: ");
        int max4 = ReadInt("Максимум: ");

        Files.GenerateBinaryFileWithIntegers(
            task4Path,
            count4,
            min4,
            max4);

        int result4 =
            Files.FindDifferenceBetweenFirstAndMaximumInBinaryFile(
                task4Path);

        Console.WriteLine("Результат: " + result4);
        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 5");

        string task5Path = "task5.xml";

        Files.GenerateToyXmlFile(task5Path);

        Console.Write(
            "Введите тип игрушки (например: Кукла): ");

        string type = Console.ReadLine();

        int age = ReadInt("Введите возраст: ");

        List<Toy> toys =
            Files.GetToysByTypeAndAge(task5Path, type, age);

        if (toys.Count == 0)
        {
            Console.WriteLine("Ничего не найдено");
        }
        else
        {
            Console.WriteLine("Результат:");

            for (int i = 0; i < toys.Count; i++)
            {
                Console.WriteLine(
                    toys[i].Type +
                    " " +
                    toys[i].Name +
                    " - " +
                    toys[i].Price);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Готово");
        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 6");

        List<int> list6 = new List<int>();

        int n6 = ReadInt("Введите количество элементов: ");

        Console.WriteLine("Введите элементы:");

        for (int i = 0; i < n6; i++)
        {
            int value = ReadInt("Элемент: ");
            list6.Add(value);
        }

        Collections.KeepOnlyFirstOccurrences(list6);

        Console.WriteLine("Результат:");

        for (int i = 0; i < list6.Count; i++)
        {
            Console.Write(list6[i] + " ");
        }

        Console.WriteLine();
        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 7");

        LinkedList<int> list7 = new LinkedList<int>();

        int n7 = ReadInt("Введите количество элементов: ");

        Console.WriteLine("Введите элементы:");

        for (int i = 0; i < n7; i++)
        {
            int number = ReadInt("Элемент: ");
            list7.AddLast(number);
        }

        int valueE = ReadInt("Введите значение E: ");

        Collections.SwapNeighborsAroundValue(
            list7,
            valueE);

        Console.WriteLine("Результат:");

        foreach (int x in list7)
        {
            Console.Write(x + " ");
        }

        Console.WriteLine();
        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 8");

        HashSet<string> allLanguages =
            new HashSet<string>();

        allLanguages.Add("Английский");
        allLanguages.Add("Немецкий");
        allLanguages.Add("Французский");
        allLanguages.Add("Испанский");

        List<HashSet<string>> workers =
            new List<HashSet<string>>();

        HashSet<string> w1 =
            new HashSet<string>();

        w1.Add("Английский");
        w1.Add("Немецкий");

        HashSet<string> w2 =
            new HashSet<string>();

        w2.Add("Английский");
        w2.Add("Французский");

        HashSet<string> w3 =
            new HashSet<string>();

        w3.Add("Английский");

        workers.Add(w1);
        workers.Add(w2);
        workers.Add(w3);

        HashSet<string> w4 =
            new HashSet<string>();

        int langCount =
            ReadInt("Сколько языков знает 4-й работник: ");

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

        Console.WriteLine(
            "Нажми Enter когда файл task9.txt будет готов");

        Console.ReadLine();

        HashSet<char> letters =
            Collections.GetStartingLettersOfWords(task9Path);

        Console.WriteLine(
            "Буквы, с которых начинаются слова:");

        foreach (char c in letters)
        {
            Console.Write(c + " ");
        }

        Console.WriteLine();
        Console.WriteLine();


        Console.WriteLine("ЗАДАНИЕ 10");

        string task10Path = "task10.txt";

        Console.WriteLine(
            "Нажми Enter когда файл task10.txt будет готов");

        Console.ReadLine();

        string result =
            Collections.GetBestStudentsFromSchool50(
                task10Path);

        Console.WriteLine("Результат:");
        Console.WriteLine(result);

        Console.WriteLine();
        Console.WriteLine("Готово");
    }
}