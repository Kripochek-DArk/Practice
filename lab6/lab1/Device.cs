class Device
{
    private bool isOn;
    private bool isWorking;

    public Device()
    {
        isOn = false;
        isWorking = false;
    }

    public Device(bool isOn, bool isWorking)
    {
        this.isOn = isOn;
        this.isWorking = isWorking;
    }

    public Device(Device other)
    {
        isOn = other.isOn;
        isWorking = other.isWorking;
    }


    public bool IsEquivalent()
    {
        return isOn == isWorking;
    }

    public override string ToString()
    {
        return $"isOn = {isOn}, isWorking = {isWorking}";
    }
    
}