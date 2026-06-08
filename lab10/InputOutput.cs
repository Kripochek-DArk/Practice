using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class InputOutput
{
    private const byte _ERRMAX = 10;
    private static char _ch;
    private static TextPosition _positionNow;
    private static List<Err> _allErrors;
    private static bool _isEndOfFile;

    private static string _line;
    private static int _lastInLine;
    private static StreamReader? _fileStream;
    private static StreamWriter? _outputStream;
    private static uint _errCount;
    private static string[] _errorTable;
    private static string _sourceFilePath;

    static InputOutput()
    {
        _positionNow = new TextPosition(0, 0);
        _allErrors = new List<Err>();
        _isEndOfFile = false;

        _line = "";
        _lastInLine = 0;
        _fileStream = null;
        _outputStream = null;
        _errCount = 0;
        _sourceFilePath = "";

        _errorTable = new string[]
        {
            "Ожидалось имя программы 'program'",
            "Пропущена точка с запятой ';'",
            "Неверный синтаксис объявления переменных 'var'",
            "Число выходит за пределы допустимого диапазона (Int32)",
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
        get { return _ch; }
    }

    public static TextPosition PositionNow
    {
        get { return _positionNow; }
    }

    public static bool IsEndOfFile
    {
        get { return _isEndOfFile; }
    }

    public static string[] ErrorTable
    {
        get { return _errorTable; }
    }

    public static void Init(string filePath, string outputPath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Ошибка: Файл {filePath} не найден");
            return;
        }

        _sourceFilePath = filePath;
        _fileStream = new StreamReader(filePath);
        _outputStream = new StreamWriter(outputPath, false, Encoding.UTF8);
        _errCount = 0;
        _isEndOfFile = false;
        _positionNow = new TextPosition(1, 0);
        _allErrors.Clear();

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

    public static void WriteCode(int code)
    {
        if (_outputStream != null)
        {
            _outputStream.Write($"{code} ");
        }
    }

    public static void Error(byte errorCode, string errorText, 
        TextPosition position)
    {
        if (_allErrors.Count < _ERRMAX)
        {
            _allErrors.Add(new Err(position, errorCode, errorText));
        }
    }

    public static void FinalizeAnalysis()
    {
        if (_fileStream != null)
        {
            _fileStream.Close();
        }
        if (_outputStream != null)
        {
            _outputStream.Close();
        }

        DumpEverything();
    }

    private static void ReadNextLine()
    {
        if (_fileStream != null && !_fileStream.EndOfStream)
        {
            _line = _fileStream.ReadLine() ?? "";
            _line += " ";
            _lastInLine = _line.Length - 1;
        }
        else
        {
            _isEndOfFile = true;
            _ch = '\0';
        }
    }

    private static void DumpEverything()
    {
        StreamReader reader;
        string currentLineText;
        uint lineNumber;
        string errorMarker;
        int totalIndent;

        reader = new StreamReader(_sourceFilePath);
        lineNumber = 1;

        while (!reader.EndOfStream)
        {
            currentLineText = reader.ReadLine() ?? "";
            Console.WriteLine($"{lineNumber.ToString().PadLeft(4)} | {currentLineText}");

            foreach (Err item in _allErrors)
            {
                if (item.ErrorPosition.LineNumber == lineNumber)
                {
                    ++_errCount;
                    errorMarker = "**";
                    if (_errCount < 10)
                    {
                        errorMarker += "0";
                    }
                    errorMarker += $"{_errCount}**";

                    totalIndent = 7 + item.ErrorPosition.CharNumber;
                    errorMarker = errorMarker.PadRight(totalIndent) + 
                        $"^ ошибка код {item.ErrorCode}: {item.ErrorText}";
                    Console.WriteLine(errorMarker);
                }
            }

            lineNumber++;
        }

        reader.Close();

        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"Всего ошибок обнаружено: {_errCount}!");
        Console.WriteLine(new string('-', 40));
    }
}