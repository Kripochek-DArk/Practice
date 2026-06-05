using System;

class Program
{
    static void Main()
    {
        string sourceFile = "test.pas";
        string outputFile = "codes.txt";

        InputOutput.Init(sourceFile, outputFile);

        if (InputOutput.IsEndOfFile)
        {
            Console.ReadKey();
            return;
        }

        LexicalAnalyzer.Analyze();

        Console.ReadKey();
    }
}