using System;
using System.IO;

class Program
{
    static void Main()
    {
        string testFileName = "test.pas";

        string testCode =
            "program Test;\n" +
            "var a, b: integer;\n" +
            "begin\n" +
            "  a := 10;\n" +
            "  b := 05;\n" +
            "  writeln(a)\n" +
            "end.";

        File.WriteAllText(testFileName, testCode);

        Console.WriteLine("Тестирование модуля ввода-вывода с таблицей ошибок");
        Console.WriteLine($"Загрузка файла: {testFileName}\n");

        InputOutput.Init(testFileName);

        Random random = new Random();

        while (!InputOutput._isEndOfFile)
        {
            if (random.Next(1, 101) <= 5)
            {
                int randomIdx = random.Next(0, InputOutput._errorTable.Length);
                byte randomErrorCode = (byte)(randomIdx + 1);
                string randomErrorText = InputOutput._errorTable[randomIdx];
                InputOutput.Error(randomErrorCode, randomErrorText, InputOutput._positionNow);
            }

            InputOutput.NextCh();
        }

        if (File.Exists(testFileName))
        {
            File.Delete(testFileName);
        }

        Console.ReadKey();
    }
}