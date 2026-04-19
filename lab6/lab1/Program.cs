using System;

class Program
{
    static void Main(string[] args)
    {
        Device d1 = new Device();

        Console.WriteLine("Введите значения для d2:");
        bool d2IsOn = ReadBool("Устройство включено? (true/false): ");
        bool d2IsWorking = ReadBool("Устройство исправно? (true/false): ");
        Device d2 = new Device(d2IsOn, d2IsWorking);

        Device d3 = new Device(d2);

        Console.WriteLine();
        Console.WriteLine("Объекты базового класса:");
        Console.WriteLine(d1);
        Console.WriteLine(d2);
        Console.WriteLine(d3);

        Console.WriteLine("Эквиваленция полей:");
        Console.WriteLine(d1.IsEquivalent());
        Console.WriteLine(d2.IsEquivalent());

        Console.WriteLine();

        SmartLamp l1 = new SmartLamp();

        Console.WriteLine("Введите значения для l2:");
        bool l2IsOn = ReadBool("Лампа включена? (true/false): ");
        bool l2IsWorking = ReadBool("Лампа исправна? (true/false): ");
        int l2Brightness = ReadInt("Введите яркость (0-100): ", 0, 100);
        string l2Name = ReadString("Введите название лампы: ");

        SmartLamp l2 = new SmartLamp(l2IsOn, l2IsWorking, l2Brightness, l2Name);
        SmartLamp l3 = new SmartLamp(l2);

        Console.WriteLine();
        Console.WriteLine("Объекты дочернего класса:");
        Console.WriteLine(l1);
        Console.WriteLine(l2);
        Console.WriteLine(l3);

        l2.TurnOn();

        int newBrightness = ReadInt("Введите новую яркость для l2 (0-100): ", 0, 100);
        l2.SetBrightness(newBrightness);

        Console.WriteLine(l2.GetInfo());
    }

    static bool ReadBool(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (bool.TryParse(input, out bool result))
            {
                return result;
            }

            Console.WriteLine("Ошибка: введите true или false.");
        }
    }

    static int ReadInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result) && result >= min && result <= max)
            {
                return result;
            }

            Console.WriteLine($"Ошибка: введите число от {min} до {max}.");
        }
    }

    static string ReadString(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Console.WriteLine("Ошибка: строка не должна быть пустой.");
        }
    }
}
