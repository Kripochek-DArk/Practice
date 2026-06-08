using System;
using System.Collections.Generic;

class SyntaxAnalyzer
{
    private static int _currentToken;
    private static HashSet<int> _typeStartTokens;
    private static HashSet<int> _statementStartTokens;
    private static HashSet<int> _globalSyncTokens;

    static SyntaxAnalyzer()
    {
        _typeStartTokens = new HashSet<int> { 6, 7, 13 }; 
        _statementStartTokens = new HashSet<int> { 4, 5, 8, 11 }; 
        _globalSyncTokens = new HashSet<int> { 2, 4, 5, 50, 60, 64 };
    }

    private static void GetNextToken()
    {
        _currentToken = LexicalAnalyzer.GetNextTokenCode();
        if (_currentToken > 0)
        {
            InputOutput.WriteCode(_currentToken);
        }
    }

    public static void Parse()
    {
        LexicalAnalyzer.Reset();
        GetNextToken();

        if (_currentToken == 1)
        {
            GetNextToken();
            if (_currentToken == LexicalAnalyzer.Identifier)
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(0, InputOutput.ErrorTable[0], 
                    InputOutput.PositionNow);
            }

            if (_currentToken == 60)
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(1, InputOutput.ErrorTable[1], 
                    InputOutput.PositionNow);
            }
        }
        else
        {
            InputOutput.Error(0, InputOutput.ErrorTable[0], 
                InputOutput.PositionNow);
            SyncTo(_globalSyncTokens);
        }

        while (_currentToken != LexicalAnalyzer.Eof)
        {
            if (_currentToken == 2)
            {
                ParseVarDeclaration();
            }
            else if (_currentToken == 50)
            {
                ParseProcedureDeclaration();
            }
            else if (_currentToken == 4)
            {
                GetNextToken();
                ParseBlock();
                
                if (_currentToken == 64) 
                {
                    GetNextToken();
                }
                else if (_currentToken != LexicalAnalyzer.Eof)
                {
                    InputOutput.Error(9, InputOutput.ErrorTable[9], 
                        InputOutput.PositionNow);
                }
            }
            else
            {
                InputOutput.Error(6, "Неверный синтаксис: токен не на своем месте", 
                    InputOutput.PositionNow);
                SyncTo(_globalSyncTokens);
                
                if (!InputOutput.IsEndOfFile && _currentToken != LexicalAnalyzer.Eof 
                    && !_globalSyncTokens.Contains(_currentToken))
                {
                    GetNextToken();
                }
            }
        }

        InputOutput.FinalizeAnalysis();
    }

    private static void ParseVarDeclaration()
    {
        GetNextToken();

        while (_currentToken == LexicalAnalyzer.Identifier)
        {
            ParseIdentifierList();

            if (_currentToken == 62) 
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(5, InputOutput.ErrorTable[5], 
                    InputOutput.PositionNow);
                SyncTo(new HashSet<int> { 6, 7, 13, 60 });
            }

            if (_typeStartTokens.Contains(_currentToken))
            {
                ParseType();
            }
            else
            {
                InputOutput.Error(7, InputOutput.ErrorTable[7], 
                    InputOutput.PositionNow);
                SyncTo(new HashSet<int> { 60, 50, 4 });
            }

            if (_currentToken == 60) 
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(1, InputOutput.ErrorTable[1], 
                    InputOutput.PositionNow);
                SyncTo(new HashSet<int> { 50, 4, 2, LexicalAnalyzer.Identifier });
                break;
            }
        }
    }

    private static void ParseIdentifierList()
    {
        if (_currentToken == LexicalAnalyzer.Identifier)
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(4, InputOutput.ErrorTable[4], 
                InputOutput.PositionNow);
            return;
        }

        while (_currentToken == 63) 
        {
            GetNextToken();
            if (_currentToken == LexicalAnalyzer.Identifier)
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(4, InputOutput.ErrorTable[4], 
                    InputOutput.PositionNow);
                SyncTo(new HashSet<int> { 62, 60 });
            }
        }
    }

    private static void ParseType()
    {
        if (_currentToken == 6 || _currentToken == 7)
        {
            GetNextToken();
        }
        else if (_currentToken == 13) 
        {
            ParseArrayDeclaration();
        }
    }

    private static void ParseArrayDeclaration()
    {
        GetNextToken();

        if (_currentToken == 69) 
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(6, "Ожидалась открывающая квадратная скобка '['", 
                InputOutput.PositionNow);
        }

        ParseIndexRange();

        while (_currentToken == 63)
        {
            GetNextToken();
            ParseIndexRange();
        }

        if (_currentToken == 70) 
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(6, "Ожидалась закрывающая квадратная скобка ']'", 
                InputOutput.PositionNow);
            SyncTo(new HashSet<int> { 14, 6, 7 });
        }

        if (_currentToken == 14) 
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(6, "Ожидалось ключевое слово 'of'", 
                InputOutput.PositionNow);
        }

        if (_currentToken == 6 || _currentToken == 7)
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(7, InputOutput.ErrorTable[7], 
                InputOutput.PositionNow);
        }
    }

    private static void ParseIndexRange()
    {
        if (_currentToken == LexicalAnalyzer.Number)
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(3, "Ожидалась константа нижней границы индекса", 
                InputOutput.PositionNow);
        }

        if (_currentToken == 64) 
        {
            GetNextToken();
            if (_currentToken == 64) 
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(6, "Ожидалось диапазонное двоеточие '..'", 
                    InputOutput.PositionNow);
            }
        }
        else
        {
            InputOutput.Error(6, "Ожидалось диапазонное двоеточие '..'", 
                InputOutput.PositionNow);
        }

        if (_currentToken == LexicalAnalyzer.Number)
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(3, "Ожидалась константа верхней границы индекса", 
                InputOutput.PositionNow);
            SyncTo(new HashSet<int> { 70, 63, 14 });
        }
    }

    private static void ParseProcedureDeclaration()
    {
        if (_currentToken == LexicalAnalyzer.Identifier)
        {
            GetNextToken();
        }

        if (_currentToken == 67) 
        {
            GetNextToken();
            if (_currentToken != 68)
            {
                ParseFormalParameters();
            }

            if (_currentToken == 68) 
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(6, "Ожидалась закрывающая скобка ')'", 
                    InputOutput.PositionNow);
                SyncTo(new HashSet<int> { 60 });
            }
        }

        if (_currentToken == 60) 
        {
            GetNextToken();
        }
        else
        {
            InputOutput.Error(1, InputOutput.ErrorTable[1], 
                InputOutput.PositionNow);
        }

        while (_currentToken == 2)
        {
            ParseVarDeclaration();
        }

        if (_currentToken == 4) 
        {
            GetNextToken();
            ParseBlock();
            if (_currentToken == 60)
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(1, "После end процедуры ожидалась ';'", 
                    InputOutput.PositionNow);
            }
        }
        else
        {
            InputOutput.Error(8, InputOutput.ErrorTable[8], 
                InputOutput.PositionNow);
        }
    }

    private static void ParseFormalParameters()
    {
        while (_currentToken == LexicalAnalyzer.Identifier || _currentToken == 2)
        {
            if (_currentToken == 2)
            {
                GetNextToken();
            }

            ParseIdentifierList();

            if (_currentToken == 62) 
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(5, InputOutput.ErrorTable[5], 
                    InputOutput.PositionNow);
            }

            if (_currentToken == 6 || _currentToken == 7)
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(7, InputOutput.ErrorTable[7], 
                    InputOutput.PositionNow);
            }

            if (_currentToken == 60)
            {
                GetNextToken();
            }
            else
            {
                break;
            }
        }
    }

    private static void ParseBlock()
    {
        while (_currentToken != 5 && _currentToken != LexicalAnalyzer.Eof && !InputOutput.IsEndOfFile)
        {
            if (_statementStartTokens.Contains(_currentToken) || 
                _currentToken == LexicalAnalyzer.Identifier || _currentToken == 51 || _currentToken == 61)
            {
                GetNextToken();
            }
            else
            {
                InputOutput.Error(6, InputOutput.ErrorTable[6], 
                    InputOutput.PositionNow);
                SyncTo(new HashSet<int> { 60, 5 });
                if (_currentToken == 60)
                {
                    GetNextToken();
                }
            }
        }

        if (_currentToken == 5) 
        {
            GetNextToken();
        }
    }

    private static void SyncTo(HashSet<int> syncSet)
    {
        while (!InputOutput.IsEndOfFile && _currentToken != LexicalAnalyzer.Eof && 
               !syncSet.Contains(_currentToken) && _currentToken != 5 && _currentToken != 4)
        {
            GetNextToken();
        }
    }
}