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
        get { return _errorPosition; }
    }

    public byte ErrorCode
    {
        get { return _errorCode; }
    }

    public string ErrorText
    {
        get { return _errorText; }
    }
}