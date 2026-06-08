using System;
using System.Collections.Generic;
using System.Text;

class LexicalAnalyzer
{
    private static int _eof = 0;
    private static int _identifier = 50;
    private static int _number = 51;

    private static Dictionary<string, int> _keywords;
    private static Stack<Tuple<char, TextPosition>> _bracketStack;
    private static bool _hasSemicolonInLine;
    private static uint _currentLine;

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
            { "real", 7 },
            { "if", 8 },
            { "then", 9 },
            { "else", 10 },
            { "while", 11 },
            { "do", 12 },
            { "array", 13 },
            { "of", 14 }
        };

        _bracketStack = new Stack<Tuple<char, TextPosition>>();
        _hasSemicolonInLine = false;
        _currentLine = 1;
    }

    public static int Eof
    {
        get { return _eof; }
    }

    public static int Identifier
    {
        get { return _identifier; }
    }

    public static int Number
    {
        get { return _number; }
    }

    public static void Reset()
    {
        _bracketStack.Clear();
        _hasSemicolonInLine = false;
        _currentLine = 1;
    }

    public static int GetNextTokenCode()
    {
        char currentCh;
        int result;

        while (!InputOutput.IsEndOfFile)
        {
            currentCh = InputOutput.Ch;

            if (InputOutput.PositionNow.LineNumber != _currentLine)
            {
                _hasSemicolonInLine = false;
                _currentLine = InputOutput.PositionNow.LineNumber;
            }

            if (char.IsWhiteSpace(currentCh) || currentCh == '\0')
            {
                InputOutput.NextCh();
                continue;
            }

            if (currentCh == '{')
            {
                ProcessCurlyComment();
                continue;
            }

            if (currentCh == '/')
            {
                InputOutput.NextCh();
                if (InputOutput.Ch == '/')
                {
                    ProcessLineComment();
                    continue;
                }
                else
                {
                    CheckSemicolonViolation();
                    InputOutput.Error(5, InputOutput.ErrorTable[4], 
                        InputOutput.PositionNow);
                    continue;
                }
            }

            if (currentCh == '(')
            {
                InputOutput.NextCh();
                if (InputOutput.Ch == '*')
                {
                    ProcessAlternativeComment();
                    continue;
                }
                else
                {
                    CheckSemicolonViolation();
                    _bracketStack.Push(new Tuple<char, TextPosition>('(', 
                        InputOutput.PositionNow));
                    return 67;
                }
            }

            if (currentCh == '[')
            {
                CheckSemicolonViolation();
                _bracketStack.Push(new Tuple<char, TextPosition>('[', 
                    InputOutput.PositionNow));
                InputOutput.NextCh();
                return 69;
            }

            if (currentCh == ')' || currentCh == ']')
            {
                CheckSemicolonViolation();
                result = CheckClosingBracket(currentCh);
                return result;
            }

            if (char.IsLetter(currentCh) || currentCh == '_')
            {
                CheckSemicolonViolation();
                return ProcessIdentifier();
            }

            if (char.IsDigit(currentCh))
            {
                CheckSemicolonViolation();
                return ProcessNumber();
            }

            return ProcessOperator();
        }

        CheckUnclosedBracketsAtEnd();
        return _eof;
    }

    private static void CheckSemicolonViolation()
    {
        if (_hasSemicolonInLine)
        {
            InputOutput.Error(13, 
                "После точки с запятой в строке не должно быть кода", 
                InputOutput.PositionNow);
        }
    }

    private static void ProcessCurlyComment()
    {
        TextPosition startPosition;
        uint startLine;

        startPosition = InputOutput.PositionNow;
        startLine = startPosition.LineNumber;
        InputOutput.NextCh(); 

        while (!InputOutput.IsEndOfFile && 
               InputOutput.PositionNow.LineNumber == startLine && 
               InputOutput.Ch != '}')
        {
            InputOutput.NextCh();
        }

        if (InputOutput.IsEndOfFile || 
            InputOutput.PositionNow.LineNumber != startLine)
        {
            InputOutput.Error(11, "Ожидался закрывающий символ '}'", 
                startPosition);
        }
        else
        {
            InputOutput.NextCh(); 
        }
    }

    private static void ProcessAlternativeComment()
    {
        TextPosition startPosition;
        uint startLine;

        startPosition = InputOutput.PositionNow;
        startLine = startPosition.LineNumber;
        InputOutput.NextCh();

        while (!InputOutput.IsEndOfFile && 
               InputOutput.PositionNow.LineNumber == startLine)
        {
            if (InputOutput.Ch == '*')
            {
                InputOutput.NextCh();
                if (InputOutput.PositionNow.LineNumber == startLine && 
                    InputOutput.Ch == ')')
                {
                    InputOutput.NextCh();
                    return;
                }
            }
            else
            {
                InputOutput.NextCh();
            }
        }

        InputOutput.Error(11, "Ожидался закрывающий символ '*)'", 
            startPosition);
    }

    private static void ProcessLineComment()
    {
        uint currentLine;
        currentLine = InputOutput.PositionNow.LineNumber;

        while (!InputOutput.IsEndOfFile && 
               InputOutput.PositionNow.LineNumber == currentLine)
        {
            InputOutput.NextCh();
        }
    }

    private static int CheckClosingBracket(char closeCh)
    {
        char expectedOpenCh;
        Tuple<char, TextPosition> openBracket;
        int code;

        expectedOpenCh = '\0';
        code = 0;

        if (closeCh == ')') expectedOpenCh = '(';
        if (closeCh == ']') expectedOpenCh = '[';

        if (_bracketStack.Count == 0)
        {
            InputOutput.Error(12, 
                $"Несоответствие парных скобок: обнаружена лишняя '{closeCh}'", 
                InputOutput.PositionNow);
        }
        else
        {
            openBracket = _bracketStack.Pop();
            if (openBracket.Item1 != expectedOpenCh)
            {
                InputOutput.Error(12, 
                    $"Ошибка: нарушен порядок скобок. Ожидалась закрывающая " +
                    $"для '{openBracket.Item1}', а встречена '{closeCh}'", 
                    InputOutput.PositionNow);
            }
            else
            {
                if (closeCh == ')') code = 68;
                if (closeCh == ']') code = 70;
            }
        }
        InputOutput.NextCh();
        return code;
    }

    private static void CheckUnclosedBracketsAtEnd()
    {
        Tuple<char, TextPosition> unclosed;

        while (_bracketStack.Count > 0)
        {
            unclosed = _bracketStack.Pop();
            InputOutput.Error(11, 
                $"Ошибка: не найдена закрывающая скобка для '{unclosed.Item1}'", 
                unclosed.Item2);
        }
    }

    private static int ProcessIdentifier()
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
            return code;
        }
        
        return _identifier;
    }

    private static int ProcessNumber()
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

        return _number;
    }

    private static int ProcessOperator()
    {
        char currentCh;
        int code;

        CheckSemicolonViolation();
        currentCh = InputOutput.Ch;
        code = 0;

        switch (currentCh)
        {
            case ';': 
                code = 60; 
                _hasSemicolonInLine = true; 
                break;
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
                return 0;
        }

        InputOutput.NextCh();
        return code;
    }
}