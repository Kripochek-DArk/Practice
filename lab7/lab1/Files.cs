using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public static class FileTasksPartOne
{
    private static Random random = new Random();

    public static void GenerateSingleNumberPerLineFile(string path, int count, int minValue, int maxValue)
    {
        StreamWriter writer = new StreamWriter(path);

        for (int i = 0; i < count; i++)
        {
            int value = random.Next(minValue, maxValue + 1);
            writer.WriteLine(value);
        }

        writer.Close();
    }

    public static void CreateFileWithDecreasedNumbers(string sourcePath, string resultPath)
    {
        StreamReader reader = new StreamReader(sourcePath);
        StreamWriter writer = new StreamWriter(resultPath);

        string line;

        while ((line = reader.ReadLine()) != null)
        {
            int number = int.Parse(line);
            writer.WriteLine(number - 1);
        }

        reader.Close();
        writer.Close();
    }

    public static void GenerateMultipleNumbersPerLineFile(
        string path,
        int rowCount,
        int numbersPerRow,
        int minValue,
        int maxValue)
    {
        StreamWriter writer = new StreamWriter(path);

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < numbersPerRow; j++)
            {
                int value = random.Next(minValue, maxValue + 1);
                writer.Write(value);

                if (j < numbersPerRow - 1)
                {
                    writer.Write(" ");
                }
            }

            writer.WriteLine();
        }

        writer.Close();
    }

    public static int FindDifferenceBetweenFirstAndMaximum(string path)
    {
        StreamReader reader = new StreamReader(path);

        string line;
        bool hasNumbers = false;
        int firstNumber = 0;
        int maximum = 0;

        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split((' '), StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length; i++)
            {
                int currentNumber = int.Parse(parts[i]);

                if (!hasNumbers)
                {
                    firstNumber = currentNumber;
                    maximum = currentNumber;
                    hasNumbers = true;
                }
                else
                {
                    if (currentNumber > maximum)
                    {
                        maximum = currentNumber;
                    }
                }
            }
        }

        reader.Close();

        if (!hasNumbers)
        {
            Console.WriteLine("Файл пуст");
            return 0;
        }       

        return firstNumber - maximum;
    }


    public static void CopyLinesStartingWithLetterB(string sourcePath, string resultPath)
    {
        StreamReader reader = new StreamReader(sourcePath);
        StreamWriter writer = new StreamWriter(resultPath);

        string line;

        while ((line = reader.ReadLine()) != null)
        {
            if (StartsWithRussianLetterB(line))
            {
                writer.WriteLine(line);
            }
        }

        reader.Close();
        writer.Close();
    }

    private static bool StartsWithRussianLetterB(string line)
    {
        if (line == null)
        {
            return false;
        }

        string trimmedLine = line.TrimStart();

        if (trimmedLine.Length == 0)
        {
            return false;
        }

        char firstCharacter = char.ToLower(trimmedLine[0]);
        return firstCharacter == 'б';
    }

    
    public static void GenerateBinaryFileWithIntegers(string path, int count, int minValue, int maxValue)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        BinaryWriter writer = new BinaryWriter(fileStream);

        for (int i = 0; i < count; i++)
        {
            int value = random.Next(minValue, maxValue + 1);
            writer.Write(value);
        }

        writer.Close();
        fileStream.Close();
    }

    public static int FindDifferenceBetweenFirstAndMaximumInBinaryFile(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(fileStream);

        if (fileStream.Length == 0)
        {
            reader.Close();
            fileStream.Close();
            throw new Exception("Бинарный файл пуст.");
        }

        int firstNumber = reader.ReadInt32();
        int maximum = firstNumber;

        while (fileStream.Position < fileStream.Length)
        {
            int currentNumber = reader.ReadInt32();

            if (currentNumber > maximum)
            {
                maximum = currentNumber;
            }
        }

        reader.Close();
        fileStream.Close();

        return firstNumber - maximum;
    }

    public static void GenerateToyXmlFile(string path)
    {
        ToyCollection collection = new ToyCollection();
        collection.Items = new Toy[8];

        collection.Items[0] = new Toy("Маша", "Кукла", 1200, 3, 6);
        collection.Items[1] = new Toy("City", "Конструктор", 2500, 6, 10);
        collection.Items[2] = new Toy("Футбольный", "Мяч", 700, 4, 8);
        collection.Items[3] = new Toy("Анна", "Кукла", 1600, 5, 7);
        collection.Items[4] = new Toy("Кубики", "Кубики", 900, 2, 6);
        collection.Items[5] = new Toy("София", "Кукла", 2100, 6, 9);
        collection.Items[6] = new Toy("Дракон", "Кукла", 1800, 4, 8);
        collection.Items[7] = new Toy("Ведьма", "Кукла", 1900, 6, 10);

        XmlSerializer serializer = new XmlSerializer(typeof(ToyCollection));

        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        serializer.Serialize(fileStream, collection);
        fileStream.Close();
    }

    public static List<Toy> GetToysByTypeAndAge(string path, string type, int age)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ToyCollection));

        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        ToyCollection collection = (ToyCollection)serializer.Deserialize(fileStream);
        fileStream.Close();

        List<Toy> result = new List<Toy>();

        if (collection.Items == null)
        {
            return result;
        }

        for (int i = 0; i < collection.Items.Length; i++)
        {
            Toy toy = collection.Items[i];

            if (IsTypeMatch(toy.Type, type) && IsSuitableForAge(toy, age))
            {
                result.Add(toy);
            }
        }

        return result;
    }

    private static bool IsTypeMatch(string toyType, string inputType)
    {
        if (toyType == null || inputType == null)
        {
            return false;
        }

        return toyType.ToLower() == inputType.ToLower();
    }

        private static bool IsSuitableForAge(Toy toy, int age)
    {
        return age >= toy.MinAge && age <= toy.MaxAge;
    }
}

[Serializable]
public class Toy
{
    public string Name;   
    public string Type;   
    public int Price;
    public int MinAge;
    public int MaxAge;

    public Toy()
    {
    }

    public Toy(string name, string type, int price, int minAge, int maxAge)
    {
        Name = name;
        Type = type;
        Price = price;
        MinAge = minAge;
        MaxAge = maxAge;
    }
}

[Serializable]
public class ToyCollection
{
    public Toy[] Items;

    public ToyCollection()
    {
    }
}
