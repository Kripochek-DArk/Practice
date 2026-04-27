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

    public static void KeepOnlyFirstOccurrences(List<int> list)
    {
        int i = 0;

        while (i < list.Count)
        {
            int j = i + 1;

            while (j < list.Count)
            {
                if (list[j] == list[i])
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

    public static void SwapNeighborsAroundValue(LinkedList<int> list, int value)
    {
        LinkedListNode<int> current = list.First;

        while (current != null)
        {
            if (current.Value == value)
            {
                if (current.Previous != null && current.Next != null)
                {
                    if (current.Previous.Value != current.Next.Value)
                    {
                        int temp = current.Previous.Value;
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
        knownByEveryone = new HashSet<string>();
        knownByAtLeastOne = new HashSet<string>();
        knownByNobody = new HashSet<string>();

        foreach (string lang in allLanguages)
        {
            knownByEveryone.Add(lang);
            knownByNobody.Add(lang);
        }

        for (int i = 0; i < workerLanguages.Count; i++)
        {
            foreach (string lang in workerLanguages[i])
            {
                knownByAtLeastOne.Add(lang);
            }
        }

        HashSet<string> toRemove = new HashSet<string>();

        foreach (string lang in allLanguages)
        {
            for (int i = 0; i < workerLanguages.Count; i++)
            {
                if (!workerLanguages[i].Contains(lang))
                {
                    toRemove.Add(lang);
                    break;
                }
            }
        }

        foreach (string lang in toRemove)
        {
            knownByEveryone.Remove(lang);
        }

        foreach (string lang in knownByAtLeastOne)
        {
            knownByNobody.Remove(lang);
        }
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

        List<string> first = new List<string>();
        List<string> second = new List<string>();

        int max1 = -1;
        int max2 = -1;

        for (int i = 1; i <= count && i < lines.Length; i++)
        {
            if (lines[i].Trim().Length == 0)
            {
                continue;
            }

            string[] parts = lines[i].Split(
                new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

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

            if (score > max1)
            {
                max2 = max1;
                second.Clear();

                for (int j = 0; j < first.Count; j++)
                {
                    second.Add(first[j]);
                }

                max1 = score;
                first.Clear();
                first.Add(fullName);
            }
            else if (score == max1)
            {
                first.Add(fullName);
            }
            else if (score > max2)
            {
                max2 = score;
                second.Clear();
                second.Add(fullName);
            }
            else if (score == max2)
            {
                second.Add(fullName);
            }
        }

        if (first.Count == 0)
        {
            return "Нет учеников школы 50";
        }

        if (first.Count > 2)
        {
            return first.Count.ToString();
        }

        if (first.Count == 2)
        {
            return first[0] + Environment.NewLine + first[1];
        }

        if (second.Count == 1)
        {
            return first[0] + Environment.NewLine + second[0];
        }

        return first[0];
    }
}