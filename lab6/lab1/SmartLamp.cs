class SmartLamp : Device
{
    private int _brightness;
    private string _name;

    public SmartLamp() : base()
    {
        _brightness = 0;
        _name = "Unnamed";
    }

    public SmartLamp(bool isOn, bool isWorking,
    int _brightness, string _name)
     : base(isOn, isWorking)
    {
        this._brightness = _brightness;
        this._name = _name;
    }

    public SmartLamp(SmartLamp other) : base(other)
    {
        _brightness = other._brightness;
        _name = other._name;
    }

    public void TurnOn()
    {
        System.Console.WriteLine($"{_name} включена");
    }

    public void SetBrightness(int value)
    {
        if (value >= 0 && value <= 100)
        {
            _brightness = value;
        }
    }

    public string GetInfo()
    {
        return $"{_name}, _brightness = {_brightness}";     
    }
    public override string ToString()
    {
        return base.ToString() + $", brightenss = {_brightness}, _name = {_name}";
    }
}