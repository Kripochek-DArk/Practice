using System;
using System.Collections.Generic;
using System.IO;
internal static class Program
{
    private const string FileName = "tests.bin";

    private static void Main()
    {
        List<Test> tests = new();

        bool isRunning = true;

        while (isRunning)
        {
            ShowMenu();

            int choice = ReadInt("Выберите пункт меню: ", 0, 9);

            try
            {
                switch (choice)
                {
                    case 1:
                        tests = TestDatabaseHelper.ReadFromFile(FileName);
                        Console.WriteLine("База данных успешно прочитана");
                        break;

                    case 2:
                        TestDatabaseHelper.ShowDatabase(tests);
                        break;

                    case 3:
                        DeleteTest(tests);
                        TestDatabaseHelper.SaveToFile(FileName, tests);
                        break;

                    case 4:
                        AddTest(tests);
                        TestDatabaseHelper.SaveToFile(FileName, tests);
                        break;

                    case 5:
                        ShowTestsBySubject(tests);
                        break;

                    case 6:
                        ShowDifficultTests(tests);
                        break;

                    case 7:
                        Console.WriteLine($"Среднее количество вопросов: {TestDatabaseHelper.GetAverageQuestionCount(tests):F2}");
                        break;

                    case 8:
                        Console.WriteLine($"Количество доступных тестов: {TestDatabaseHelper.GetAvailableTestCount(tests)}");
                        break;

                    case 9:
                        TestDatabaseHelper.SaveToFile(FileName, tests);
                        Console.WriteLine("База данных сохранена");
                        break;

                    case 0:
                        isRunning = false;
                        break;
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка при работе с файлом");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Ошибка: {exception.Message}");
            }

            Console.WriteLine();
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("=== База данных: сборник тестов ===");
        Console.WriteLine("1. Прочитать БД из бинарного файла");
        Console.WriteLine("2. Просмотреть БД");
        Console.WriteLine("3. Удалить тест по ID");
        Console.WriteLine("4. Добавить тест");
        Console.WriteLine("5. Запрос: тесты по предмету");
        Console.WriteLine("6. Запрос: сложные тесты");
        Console.WriteLine("7. Запрос: среднее количество вопросов");
        Console.WriteLine("8. Запрос: количество доступных тестов");
        Console.WriteLine("9. Сохранить БД в файл");
        Console.WriteLine("0. Выход");
    }

    private static void AddTest(List<Test> tests)
    {
        int id = ReadInt("ID: ", 1, int.MaxValue);
        string subject = ReadString("Предмет: ");
        string topic = ReadString("Тема: ");
        int questionCount = ReadInt("Количество вопросов: ", 1, 500);
        double difficulty = ReadDouble("Сложность от 1 до 10: ", 1, 10);
        bool isAvailable = ReadBool("Тест доступен? (д/н): ");

        Test test = new(id, subject, topic, questionCount, difficulty, isAvailable);

        if (TestDatabaseHelper.AddTest(tests, test))
        {
            Console.WriteLine("Тест добавлен");
        }
        else
        {
            Console.WriteLine("Тест с таким ID уже существует");
        }
    }

    private static void DeleteTest(List<Test> tests)
    {
        int id = ReadInt("Введите ID для удаления: ", 1, int.MaxValue);

        if (TestDatabaseHelper.DeleteById(tests, id))
        {
            Console.WriteLine("Тест удален");
        }
        else
        {
            Console.WriteLine("Тест с таким ID не найден");
        }
    }

    private static void ShowTestsBySubject(List<Test> tests)
    {
        string subject = ReadString("Введите предмет: ");

        List<Test> result = TestDatabaseHelper.GetTestsBySubject(tests, subject);

        TestDatabaseHelper.ShowDatabase(result);
    }

    private static void ShowDifficultTests(List<Test> tests)
    {
        double minDifficulty = ReadDouble("Минимальная сложность: ", 1, 10);

        List<Test> result = TestDatabaseHelper.GetDifficultTests(tests, minDifficulty);

        TestDatabaseHelper.ShowDatabase(result);
    }

    private static int ReadInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Введите целое число от {min} до {max}");
        }
    }

    private static double ReadDouble(string message, double min, double max)
    {
        while (true)
        {
            Console.Write(message);

            if (double.TryParse(Console.ReadLine(), out double value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Введите число от {min} до {max}");
        }
    }

    private static string ReadString(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? value = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.WriteLine("Строка не должна быть пустой");
        }
    }

    private static bool ReadBool(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? value = Console.ReadLine()?.Trim().ToLower();

            if (value == "д" || value == "да")
            {
                return true;
            }

            if (value == "н" || value == "нет")
            {
                return false;
            }

            Console.WriteLine("Введите 'д' или 'н'");
        }
    }
}