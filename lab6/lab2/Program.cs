using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите первое время");
        Time firstTime = ReadTime();

        Console.WriteLine("Введите второе время");
        Time secondTime = ReadTime();

        Console.WriteLine();
        Console.WriteLine("Первое время: " + firstTime);
        Console.WriteLine("Второе время: " + secondTime);

        Console.WriteLine();
        Console.WriteLine("Вычитание:");
        Console.WriteLine(firstTime + " - " + secondTime + " = " + firstTime.Subtract(secondTime));

        Console.WriteLine();
        Console.WriteLine("Оператор -- для первого времени:");
        Time decreasedTime = new Time(firstTime);
        decreasedTime--;
        Console.WriteLine("Результат: " + decreasedTime);

        Console.WriteLine();
        Console.WriteLine("Явное приведение к byte:");
        byte hours = (byte)firstTime;
        Console.WriteLine("Часы первого времени: " + hours);

        Console.WriteLine();
        Console.WriteLine("Неявное приведение к bool:");
        Console.WriteLine((bool)firstTime);
        Console.WriteLine((bool)secondTime);

        Console.WriteLine();
        Console.Write("Введите количество минут для прибавления: ");
        uint minutesToAdd = ReadUnsignedInt();

        Console.WriteLine();
        Console.WriteLine("Time + uint:");
        Console.WriteLine((firstTime + minutesToAdd).ToString());

        Console.WriteLine();
        Console.WriteLine("uint + Time:");
        Console.WriteLine((minutesToAdd + firstTime).ToString());

        Console.WriteLine();
        Console.WriteLine("Time + Time:");
        Console.WriteLine((firstTime + secondTime).ToString());

    }

    static Time ReadTime()
    {
        byte hours = ReadHours();
        byte minutes = ReadMinutes();
        return new Time(hours, minutes);
    }

    static byte ReadHours()
    {
        while (true)
        {
            Console.Write("Введите часы (0-23): ");
            string input = Console.ReadLine();

            byte value;
            bool isParsed = byte.TryParse(input, out value);

            if (isParsed && value <= 23)
            {
                return value;
            }

            Console.WriteLine("Ошибка: введите корректные часы.");
        }
    }

    static byte ReadMinutes()
    {
        while (true)
        {
            Console.Write("Введите минуты (0-59): ");
            string input = Console.ReadLine();

            byte value;
            bool isParsed = byte.TryParse(input, out value);

            if (isParsed && value <= 59)
            {
                return value;
            }

            Console.WriteLine("Ошибка: введите корректные минуты.");
        }
    }

    static uint ReadUnsignedInt()
    {
        while (true)
        {
            string input = Console.ReadLine();

            uint value;
            bool isParsed = uint.TryParse(input, out value);

            if (isParsed)
            {
                return value;
            }

            Console.Write("Ошибка: введите корректное неотрицательное число: ");
        }
    }
}