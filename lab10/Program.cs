using System;
using System.IO;

class Program
{
    static void Main()
    {
        string sourceFile = "test.pas"; 

        InputOutput.Init(sourceFile);

        if (InputOutput.IsEndOfFile)
        {
            Console.ReadKey();
            return;
        }

        Random random = new Random();

        int randomIdx;
        byte randomErrorCode;
        string randomErrorText;
        while (!InputOutput.IsEndOfFile)
        {
            if (random.Next(1, 101) <= 4)
            {
                randomIdx = random.Next(0, InputOutput.ErrorTable.Length);

                randomErrorCode = (byte)(randomIdx + 1);
                randomErrorText = InputOutput.ErrorTable[randomIdx];

                InputOutput.Error(randomErrorCode, randomErrorText, InputOutput.PositionNow);
            }

            InputOutput.NextCh();
        }

        Console.ReadKey();
    }
}