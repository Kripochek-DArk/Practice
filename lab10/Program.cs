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


        InputOutput.Init(testFileName);

        Random random = new Random();

        while (!InputOutput._isEndOfFile)
        {
            if (random.Next(1, 101) <= 5)
            {
                byte randomErrorCode = (byte)random.Next(1, 11);
                InputOutput.Error(randomErrorCode, InputOutput._positionNow);
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