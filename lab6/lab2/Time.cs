class Time
{
    private byte hours;
    private byte minutes;

    public byte Hours
    {
        get
        {
            return hours;
        }
        set
        {
            if (value <= 23)
            {
                hours = value;
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
            return minutes;
        }
        set
        {
            if (value <= 59)
            {
                minutes = value;
            }
            else
            {
                throw new ArgumentException("Неверный диапазон минут.");
            }
        }
    }

    public Time()
    {
        hours = 0;
        minutes = 0;
    }

    public Time(byte hours, byte minutes)
    {
        Hours = hours;
        Minutes = minutes;
    }

    public Time(Time other)
    {
        if (other == null)
        {
            throw new ArgumentException("Ошибка времени.");
        }

        hours = other.hours;
        minutes = other.minutes;
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
        return hours.ToString("D2") + ":" + minutes.ToString("D2");
    }

    public static Time operator --(Time time)
    {
        int totalMinutes = time.hours * 60 + time.minutes;
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
        return time.hours;
    }

    public static implicit operator bool(Time time)
    {
        return time.hours != 0 || time.minutes != 0;
    }

    public static Time operator +(Time time, uint value)
    {
        int totalMinutes = time.hours * 60 + time.minutes;
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
        int firstTotalMinutes = first.hours * 60 + first.minutes;
        int secondTotalMinutes = second.hours * 60 + second.minutes;
        int resultTotalMinutes = firstTotalMinutes + secondTotalMinutes;

        resultTotalMinutes %= 24 * 60;

        byte resultHours = (byte)(resultTotalMinutes / 60);
        byte resultMinutes = (byte)(resultTotalMinutes % 60);

        return new Time(resultHours, resultMinutes);
    }

    private int ToTotalMinutes()
    {
        return hours * 60 + minutes;
    }

    private static Time FromTotalMinutes(int totalMinutes)
    {
        byte resultHours = (byte)(totalMinutes / 60);
        byte resultMinutes = (byte)(totalMinutes % 60);

        return new Time(resultHours, resultMinutes);
    }
}