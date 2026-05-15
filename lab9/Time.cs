using System;

public class Time
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
                throw new ArgumentException("Часы должны быть в диапазоне от 0 до 23.");
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
                throw new ArgumentException("Минуты должны быть в диапазоне от 0 до 59.");
            }
        }
    }

    public Time()
    {
        _hours = 0;
        _minutes = 0;
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
            throw new ArgumentException("Объект времени не может быть пустым.");
        }

        _hours = other._hours;
        _minutes = other._minutes;
    }

    public Time Subtract(Time other)
    {
        if (other == null)
        {
            throw new ArgumentException("Объект времени не может быть пустым.");
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
        if (time == null)
        {
            throw new ArgumentException("Объект времени не может быть пустым.");
        }

        int totalMinutes = time.ToTotalMinutes();
        totalMinutes--;

        if (totalMinutes < 0)
        {
            totalMinutes = 24 * 60 - 1;
        }

        return FromTotalMinutes(totalMinutes);
    }

    public static explicit operator byte(Time time)
    {
        if (time == null)
        {
            throw new ArgumentException("Объект времени не может быть пустым.");
        }

        return time._hours;
    }

    public static implicit operator bool(Time time)
    {
        if (time == null)
        {
            return false;
        }

        return time._hours != 0 || time._minutes != 0;
    }

    public static Time operator +(Time time, uint value)
    {
        if (time == null)
        {
            throw new ArgumentException("Объект времени не может быть пустым.");
        }

        int totalMinutes = time.ToTotalMinutes();
        totalMinutes += (int)(value % (24 * 60));
        totalMinutes %= 24 * 60;

        return FromTotalMinutes(totalMinutes);
    }

    public static Time operator +(uint value, Time time)
    {
        return time + value;
    }

    public static Time operator +(Time first, Time second)
    {
        if (first == null || second == null)
        {
            throw new ArgumentException("Объект времени не может быть пустым.");
        }

        int resultTotalMinutes = first.ToTotalMinutes() + second.ToTotalMinutes();
        resultTotalMinutes %= 24 * 60;

        return FromTotalMinutes(resultTotalMinutes);
    }

    private int ToTotalMinutes()
    {
        return _hours * 60 + _minutes;
    }

    private static Time FromTotalMinutes(int totalMinutes)
    {
        totalMinutes %= 24 * 60;

        if (totalMinutes < 0)
        {
            totalMinutes += 24 * 60;
        }

        byte hours = (byte)(totalMinutes / 60);
        byte minutes = (byte)(totalMinutes % 60);

        return new Time(hours, minutes);
    }
}