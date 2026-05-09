using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
internal static class TestDatabaseHelper
{
    public static List<Test> ReadFromFile(string fileName)
    {
        List<Test> tests = new();

        if (!File.Exists(fileName))
        {
            return tests;
        }

        using BinaryReader reader = new(File.Open(fileName, FileMode.Open));

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            Test test = new(
                reader.ReadInt32(),
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadInt32(),
                reader.ReadDouble(),
                reader.ReadBoolean());

            tests.Add(test);
        }

        return tests;
    }

    public static void SaveToFile(string fileName, List<Test> tests)
    {
        using BinaryWriter writer = new(File.Open(fileName, FileMode.Create));

        foreach (Test test in tests)
        {
            writer.Write(test.Id);
            writer.Write(test.Subject);
            writer.Write(test.Topic);
            writer.Write(test.QuestionCount);
            writer.Write(test.Difficulty);
            writer.Write(test.IsAvailable);
        }
    }

    public static void ShowDatabase(List<Test> tests)
    {
        if (!tests.Any())
        {
            Console.WriteLine("База данных пуста");
            return;
        }

        foreach (Test test in tests)
        {
            Console.WriteLine(test);
        }
    }

    public static bool AddTest(List<Test> tests, Test test)
    {
        bool idExists = tests.Any(item => item.Id == test.Id);

        if (idExists)
        {
            return false;
        }

        tests.Add(test);
        return true;
    }

    public static bool DeleteById(List<Test> tests, int id)
    {
        Test? test = tests.FirstOrDefault(item => item.Id == id);

        if (test == null)
        {
            return false;
        }

        tests.Remove(test);
        return true;
    }

    public static List<Test> GetTestsBySubject(List<Test> tests, string subject)
    {
        return tests
            .Where(test => test.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public static List<Test> GetDifficultTests(List<Test> tests, double minDifficulty)
    {
        return tests
            .Where(test => test.Difficulty >= minDifficulty)
            .OrderByDescending(test => test.Difficulty)
            .ToList();
    }

    public static double GetAverageQuestionCount(List<Test> tests)
    {
        return tests.Any()
            ? tests.Average(test => test.QuestionCount)
            : 0;
    }

    public static int GetAvailableTestCount(List<Test> tests)
    {
        return tests.Count(test => test.IsAvailable);
    }
}