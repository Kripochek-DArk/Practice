class Time
{
    private byte _hours;
    private byte _minutes;

    public byte Hours
    {
        get
        {
            return _hours;
        }
        set
        {
            if (value <= 23)
            {
                _hours = value;
            }
            else
            {
                throw new ArgumentException("Неверный диапазон часов.");
            }
        }
    }

    public byte Minutes
    {
        get
        {
            return _minutes;
        }
        set
        {
            if (value <= 59)
            {
                _minutes = value;
            }
            else
            {
                throw new ArgumentException("Неверный диапазон минут.");
            }
        }
    }

    public Time()
    {
        _hours = 0;
        _minutes = 0;
    }

    public Time(byte _hours, byte _minutes)
    {
        Hours = _hours;
        Minutes = _minutes;
    }

    public Time(Time other)
    {
        if (other == null)
        {
            throw new ArgumentException("Ошибка времени.");
        }

        _hours = other._hours;
        _minutes = other._minutes;
    }

    public Time Subtract(Time other)
    {
        if (other == null)
        {
            throw new ArgumentException("Ошибка времени.");
        }

        int currentTotalMinutes = ToTotalMinutes();
        int otherTotalMinutes = other.ToTotalMinutes();
        int resultTotalMinutes = currentTotalMinutes - otherTotalMinutes;

        if (resultTotalMinutes < 0)
        {
            resultTotalMinutes += 24 * 60;
        }

        return FromTotalMinutes(resultTotalMinutes);
    }

    public override string ToString()
    {
        return _hours.ToString("D2") + ":" + _minutes.ToString("D2");
    }

    public static Time operator --(Time time)
    {
        int totalMinutes = time._hours * 60 + time._minutes;
        totalMinutes--;

        if (totalMinutes < 0)
        {
            totalMinutes = 24 * 60 - 1;
        }

        byte newHours = (byte)(totalMinutes / 60);
        byte newMinutes = (byte)(totalMinutes % 60);

        return new Time(newHours, newMinutes);
    }

    public static explicit operator byte(Time time)
    {
        return time._hours;
    }

    public static implicit operator bool(Time time)
    {
        return time._hours != 0 || time._minutes != 0;
    }

    public static Time operator +(Time time, uint value)
    {
        int totalMinutes = time._hours * 60 + time._minutes;
        totalMinutes += (int)value;
        totalMinutes %= 24 * 60;

        byte newHours = (byte)(totalMinutes / 60);
        byte newMinutes = (byte)(totalMinutes % 60);

        return new Time(newHours, newMinutes);
    }

    public static Time operator +(uint value, Time time)
    {
        return time + value;
    }

    public static Time operator +(Time first, Time second)
    {
        int firstTotalMinutes = first._hours * 60 + first._minutes;
        int secondTotalMinutes = second._hours * 60 + second._minutes;
        int resultTotalMinutes = firstTotalMinutes + secondTotalMinutes;

        resultTotalMinutes %= 24 * 60;

        byte resultHours = (byte)(resultTotalMinutes / 60);
        byte resultMinutes = (byte)(resultTotalMinutes % 60);

        return new Time(resultHours, resultMinutes);
    }

    private int ToTotalMinutes()
    {
        return _hours * 60 + _minutes;
    }

    private static Time FromTotalMinutes(int totalMinutes)
    {
        byte resultHours = (byte)(totalMinutes / 60);
        byte resultMinutes = (byte)(totalMinutes % 60);

        return new Time(resultHours, resultMinutes);
    }
}