using System;
using System.Collections.Generic;
using System.Text;

class LexicalAnalyzer
{
    private static Dictionary<string, int> _keywords;

    static LexicalAnalyzer()
    {
        _keywords = new Dictionary<string, int>(
            StringComparer.OrdinalIgnoreCase)
        {
            { "program", 1 },
            { "var", 2 },
            { "const", 3 },
            { "begin", 4 },
            { "end", 5 },
            { "integer", 6 },
            { "real", 7 }
        };
    }

    public static void Analyze()
    {
        char currentCh;

        while (!InputOutput.IsEndOfFile)
        {
            currentCh = InputOutput.Ch;

            if (char.IsWhiteSpace(currentCh) || currentCh == '\0')
            {
                InputOutput.NextCh();
                continue;
            }

            if (char.IsLetter(currentCh) || currentCh == '_')
            {
                ProcessIdentifier();
                continue;
            }

            if (char.IsDigit(currentCh))
            {
                ProcessNumber();
                continue;
            }

            ProcessOperator();
        }
    }

    private static void ProcessIdentifier()
    {
        StringBuilder builder;
        string lexeme;
        int code;

        builder = new StringBuilder();

        while (!InputOutput.IsEndOfFile && 
               (char.IsLetterOrDigit(InputOutput.Ch) || InputOutput.Ch == '_'))
        {
            builder.Append(InputOutput.Ch);
            InputOutput.NextCh();
        }

        lexeme = builder.ToString();

        if (_keywords.TryGetValue(lexeme, out code))
        {
            InputOutput.WriteCode(code);
        }
        else
        {
            InputOutput.WriteCode(50); 
        }
    }

    private static void ProcessNumber()
    {
        StringBuilder builder;
        string lexeme;
        long parsedValue;
        TextPosition startPosition;

        builder = new StringBuilder();
        startPosition = InputOutput.PositionNow;

        while (!InputOutput.IsEndOfFile && char.IsDigit(InputOutput.Ch))
        {
            builder.Append(InputOutput.Ch);
            InputOutput.NextCh();
        }

        lexeme = builder.ToString();

        if (long.TryParse(lexeme, out parsedValue))
        {
            if (parsedValue > int.MaxValue || parsedValue < int.MinValue)
            {
                InputOutput.Error(4, InputOutput.ErrorTable[3], startPosition);
            }
        }
        else
        {
            InputOutput.Error(4, InputOutput.ErrorTable[3], startPosition);
        }

        InputOutput.WriteCode(51);
    }

    private static void ProcessOperator()
    {
        char currentCh;
        int code;

        currentCh = InputOutput.Ch;
        code = 0;

        switch (currentCh)
        {
            case ';': code = 60; break;
            case '=': code = 61; break;
            case ':': code = 62; break;
            case ',': code = 63; break;
            case '.': code = 64; break;
            case '+': code = 65; break;
            case '-': code = 66; break;
            default:
                InputOutput.Error(5, InputOutput.ErrorTable[4], 
                    InputOutput.PositionNow);
                InputOutput.NextCh();
                return;
        }

        InputOutput.WriteCode(code);
        InputOutput.NextCh();
    }
}