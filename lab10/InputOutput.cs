using System;
using System.Collections.Generic;
using System.IO;

struct TextPosition
{
    private uint _lineNumber;
    private byte _charNumber;


    public TextPosition(uint ln = 0, byte c = 0)
    {
        _lineNumber = ln;
        _charNumber = c;
    }

    public uint LineNumber
    {
        get
        {
            return _lineNumber;
        }
    }

    public byte CharNumber
    {
        get
        {
            return _charNumber;
        }
    }

    public void AdvanceChar()
    {
        _charNumber++;
    }

    public void ResetCharAndIncrementLine()
    {
        _charNumber = 0;
        _lineNumber++;
    }
}

struct Err
{
    private TextPosition _errorPosition;
    private byte _errorCode;
    private string _errorText;

    public Err(TextPosition errorPosition, byte errorCode, string errorText)
    {
        _errorPosition = errorPosition;
        _errorCode = errorCode;
        _errorText = errorText;
    }

    public TextPosition ErrorPosition
    {
        get
        {
            return _errorPosition;
        }
    }

    public byte ErrorCode
    {
        get
        {
            return _errorCode;
        }
    }

    public string ErrorText
    {
        get
        {
            return _errorText;
        }
    }
}

class InputOutput
{
    private const byte _ERRMAX = 10;
    private static char _ch;
    private static TextPosition _positionNow;
    private static List<Err> _err;
    private static bool _isEndOfFile;

    private static string _line;
    private static int _lastInLine;
    private static StreamReader? _fileStream; 
    private static uint _errCount;
    private static string[] _errorTable;

    
    static InputOutput()
    {
        _positionNow = new TextPosition(0, 0);
        _err = new List<Err>();
        _isEndOfFile = false;

        _line = "";
        _lastInLine = 0;
        _fileStream = null;
        _errCount = 0;

        _errorTable = new string[]
        {
            "Ожидалось имя программы 'program'",
            "Пропущена точка с запятой ';'",
            "Неверный синтаксис объявления переменных 'var'",
            "Неверная запись константы или числа",
            "Неизвестный идентификатор",
            "Ожидалось двоеточие ':'",
            "Синтаксическая ошибка в теле программы",
            "Несоответствие типов данных",
            "Ожидался оператор 'begin'",
            "Пропущена точка в конце программы '.'"
        };
    }
    public static char Ch
    {
        get
        {
            return _ch;
        }
    }

    public static TextPosition PositionNow
    {
        get
        {
            return _positionNow;
        }
    }

    public static bool IsEndOfFile
    {
        get
        {
            return _isEndOfFile;
        }
    }

    public static string[] ErrorTable
    {
        get
        {
            return _errorTable;
        }
    }

    public static void Init(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Ошибка: Файл {filePath} не найден");
            return;
        }

        _fileStream = new StreamReader(filePath);
        _errCount = 0;
        _isEndOfFile = false;
        _positionNow = new TextPosition(1, 0);
        _err = new List<Err>();

        if (!_fileStream.EndOfStream)
        {
            _line = _fileStream.ReadLine() ?? ""; 
            _line += " ";
            _lastInLine = _line.Length - 1;
            _ch = _line[0];
        }
        else
        {
            _isEndOfFile = true;
            _ch = '\0';
        }
    }

    public static void NextCh()
    {
        if (_isEndOfFile)
        {
            _ch = '\0';
            return;
        }

        if (_positionNow.CharNumber >= _lastInLine)
        {
            ListThisLine();
            if (_err.Count > 0)
            {
                ListErrors();
            }

            ReadNextLine();

            if (!_isEndOfFile)
            {
                _positionNow.ResetCharAndIncrementLine();
                _ch = _line[0];
            }
        }
        else
        {
            _positionNow.AdvanceChar();
            _ch = _line[_positionNow.CharNumber];
        }
    }

    public static void Error(byte _errorCode,
    string _errorText, TextPosition position)
    {
        if (_err.Count <= _ERRMAX)
        {
            Err e = new Err(position, _errorCode, _errorText);
            _err.Add(e);
        }
    }

    private static void ListThisLine()
    {
        Console.WriteLine(
            $"{_positionNow.LineNumber.ToString().PadLeft(4)}" +
            $" | {_line.TrimEnd()}");
    }

    private static void ReadNextLine()
    {
        if (_fileStream != null && !_fileStream.EndOfStream)
        {
            _line = _fileStream.ReadLine() ?? ""; 
            _line += " ";
            _lastInLine = _line.Length - 1;
            _err = new List<Err>();
        }
        else
        {
            _isEndOfFile = true;
            _ch = '\0';
            if (_fileStream != null)
            {
                _fileStream.Close();
            }
            End();
        }
    }

    private static void End()
    {
        Console.WriteLine(new string('-', 40));
        Console.WriteLine(
        $"Всего ошибок обнаружено: {_errCount}!");
        Console.WriteLine(new string('-', 40));
    }

    private static void ListErrors()
    {
        string s;
        int totalIndent;
        foreach (Err item in _err)
        {
            ++_errCount;
            s = "**";
            if (_errCount < 10)
            {
                s += "0";
            }
            s += $"{_errCount}**";

            totalIndent = 7 + item.ErrorPosition.CharNumber;
            s = s.PadRight(totalIndent) + $"^ ошибка код {item.ErrorCode}: {item.ErrorText}";
            Console.WriteLine(s);
        }
    }
}