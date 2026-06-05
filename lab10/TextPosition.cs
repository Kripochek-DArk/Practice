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
        get { return _lineNumber; }
    }

    public byte CharNumber
    {
        get { return _charNumber; }
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