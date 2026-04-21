class Device
{
    private bool _isOn;
    private bool _isWorking;

    public Device()
    {
        _isOn = false;
        _isWorking = false;
    }

    public Device(bool _isOn, bool _isWorking)
    {
        this._isOn = _isOn;
        this._isWorking = _isWorking;
    }

    public Device(Device other)
    {
        _isOn = other._isOn;
        _isWorking = other._isWorking;
    }


    public bool IsEquivalent()
    {
        return _isOn == _isWorking;
    }

    public override string ToString()
    {
        return $"_isOn = {_isOn}, _isWorking = {_isWorking}";
    }
    
}