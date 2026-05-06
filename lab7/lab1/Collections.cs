using System;
using System.Collections.Generic;
using System.IO;

public static class Collections
{
    private static bool CheckFileForRead(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден: " + path);
            return false;
        }

        return true;
    }

    public static void KeepOnlyFirstOccurrences<T>(List<T> list)
    {
        int i = 0;

        while (i < list.Count)
        {
            int j = i + 1;

            while (j < list.Count)
            {
                if (EqualityComparer<T>.Default.Equals(list[j], list[i]))
                {
                    list.RemoveAt(j);
                }
                else
                {
                    j++;
                }
            }

            i++;
        }
    }

    public static void SwapNeighborsAroundValue<T>(LinkedList<T> list, T value)
    {
        LinkedListNode<T> current = list.First;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
                if (current.Previous != null && current.Next != null)
                {
                    if (!EqualityComparer<T>.Default.Equals(
                        current.Previous.Value,
                        current.Next.Value))
                    {
                        T temp = current.Previous.Value;
                        current.Previous.Value = current.Next.Value;
                        current.Next.Value = temp;
                    }
                }
            }

            current = current.Next;
        }
    }

    public static void AnalyzeWorkerLanguages(
    HashSet<string> allLanguages,
    List<HashSet<string>> workerLanguages,
    out HashSet<string> knownByEveryone,
    out HashSet<string> knownByAtLeastOne,
    out HashSet<string> knownByNobody)
    {
        knownByEveryone = new HashSet<string>(allLanguages);
        knownByAtLeastOne = new HashSet<string>();
        knownByNobody = new HashSet<string>(allLanguages);

        for (int i = 0; i < workerLanguages.Count; i++)
        {
            knownByEveryone.IntersectWith(workerLanguages[i]);
            knownByAtLeastOne.UnionWith(workerLanguages[i]);
        }

        knownByNobody.ExceptWith(knownByAtLeastOne);
    }

    public static HashSet<char> GetStartingLettersOfWords(string path)
    {
        HashSet<char> result = new HashSet<char>();

        if (!CheckFileForRead(path))
        {
            return result;
        }

        string text = File.ReadAllText(path);
        bool insideWord = false;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (char.IsLetter(c))
            {
                if (!insideWord)
                {
                    result.Add(char.ToLower(c));
                    insideWord = true;
                }
            }
            else
            {
                insideWord = false;
            }
        }

        return result;
    }

    public static string GetBestStudentsFromSchool50(string path)
    {
        if (!CheckFileForRead(path))
        {
            return "Файл не найден";
        }

        string[] lines = File.ReadAllLines(path);

        if (lines.Length == 0)
        {
            return "Файл пуст";
        }

        int count = int.Parse(lines[0]);

        SortedList<int, List<string>> studentsByScore =
            new SortedList<int, List<string>>();

        for (int i = 1; i <= count && i < lines.Length; i++)
        {
            if (lines[i].Trim().Length == 0)
            {
                continue;
            }

            string[] parts = lines[i].Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 4)
            {
                continue;
            }

            string surname = parts[0];
            string name = parts[1];
            int school = int.Parse(parts[2]);
            int score = int.Parse(parts[3]);

            if (school != 50)
            {
                continue;
            }

            string fullName = surname + " " + name;

            if (!studentsByScore.ContainsKey(score))
            {
                studentsByScore.Add(score, new List<string>());
            }

            studentsByScore[score].Add(fullName);
        }

        if (studentsByScore.Count == 0)
        {
            return "Нет учеников школы 50";
        }

        int maxScore = studentsByScore.Keys[studentsByScore.Count - 1];
        List<string> first = studentsByScore[maxScore];

        if (first.Count > 2)
        {
            return first.Count.ToString();
        }

        if (first.Count == 2)
        {
            return first[0] + Environment.NewLine + first[1];
        }

        if (studentsByScore.Count < 2)
        {
            return first[0];
        }

        int secondScore = studentsByScore.Keys[studentsByScore.Count - 2];
        List<string> second = studentsByScore[secondScore];

        if (second.Count == 1)
        {
            return first[0] + Environment.NewLine + second[0];
        }

        return first[0];
    }
}
/*
5 публичные поля
6-7 сделанно для Int 
8 надо операции именно над множествами 
10 нет словаря
*/