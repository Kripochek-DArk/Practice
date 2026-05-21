using System;
using System.Collections.Generic;
using System.IO;

struct TextPosition
{
    public uint _lineNumber;
    public byte _charNumber;

    public TextPosition(uint ln = 0, byte c = 0)
    {
        _lineNumber = ln;
        _charNumber = c;
    }
}

struct Err
{
    public TextPosition _errorPosition;
    public byte _errorCode;

    public Err(TextPosition _errorPosition, byte _errorCode)
    {
        this._errorPosition = _errorPosition;
        this._errorCode = _errorCode;
    }
}

class InputOutput
{
    public static char _ch { get; set; }
    public static TextPosition _positionNow = new TextPosition(0, 0);
    public static List<Err> _err = new List<Err>();
    public static bool _isEndOfFile { get; private set; } = false;

    private static string _line = "";
    private static int _lastInLine = 0;
    private static StreamReader _fileStream { get; set; }
    private static uint _errCount = 0;

    public static void Init(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Ошибка: Файл {filePath} не найден.");
            return;
        }

        _fileStream = new StreamReader(filePath);
        _errCount = 0;
        _isEndOfFile = false;
        _positionNow = new TextPosition(1, 0);
        _err = new List<Err>();

        if (!_fileStream.EndOfStream)
        {
            _line = _fileStream.ReadLine();
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

        if (_positionNow._charNumber >= _lastInLine)
        {
            ListThisLine();
            if (_err.Count > 0)
            {
                ListErrors();
            }

            ReadNextLine();

            if (!_isEndOfFile)
            {
                _positionNow._lineNumber++;
                _positionNow._charNumber = 0;
                _ch = _line[0];
            }
        }
        else
        {
            _positionNow._charNumber++;
            _ch = _line[_positionNow._charNumber];
        }
    }

    public static void Error(byte _errorCode, TextPosition position)
    {
        
        Err e = new Err(position, _errorCode);
        _err.Add(e);
        
    }

    private static void ListThisLine()
    {
        Console.WriteLine(
            $"{_positionNow._lineNumber.ToString().PadLeft(4)}" +
            $" | {_line.TrimEnd()}");
    }

    private static void ReadNextLine()
    {
        if (!_fileStream.EndOfStream)
        {
            _line = _fileStream.ReadLine();
            _line += " ";
            _lastInLine = _line.Length - 1;
            _err = new List<Err>();
        }
        else
        {
            _isEndOfFile = true;
            _ch = '\0';
            _fileStream.Close();
            End();
        }
    }

    private static void End()
    {
        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"Всего ошибок обнаружено: {_errCount}!");
        Console.WriteLine(new string('-', 40));
    }

    private static void ListErrors()
    {
        foreach (Err item in _err)
        {
            ++_errCount;
            string s = "**";
            if (_errCount < 10)
            {
                s += "0";
            }
            s += $"{_errCount}**";

            int totalIndent = 7 + item._errorPosition._charNumber;
            s = s.PadRight(totalIndent) + $"^ ошибка код {item._errorCode}";
            Console.WriteLine(s);
        }
    }
}