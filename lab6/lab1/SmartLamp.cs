class SmartLamp : Device
{
    private int brightness;
    private string name;

    public SmartLamp() : base()
    {
        brightness = 0;
        name = "Unnamed";
    }

    public SmartLamp(bool isOn, bool isWorking, int brightness, string name) : base(isOn, isWorking)
    {
        this.brightness = brightness;
        this.name = name;
    }

    public SmartLamp(SmartLamp other) : base(other)
    {
        brightness = other.brightness;
        name = other.name;
    }

    public void TurnOn()
    {
        System.Console.WriteLine($"{name} включена");
    }

    public void SetBrightness(int value)
    {
        if (value >= 0 && value <= 100)
        {
            brightness = value;
        }
    }

    public string GetInfo()
    {
        return $"{name}, brightness = {brightness}";     
    }
    public override string ToString()
    {
        return base.ToString() + $", brightenss = {brightness}, name = {name}";
    }
}