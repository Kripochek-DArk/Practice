using System;
using System.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void SubtractButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Time firstTime = ReadFirstTime();
            Time secondTime = ReadSecondTime();

            Time result = firstTime.Subtract(secondTime);

            ShowResult(firstTime + " - " + secondTime + " = " + result);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void DecrementButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Time firstTime = ReadFirstTime();
            Time result = new Time(firstTime);

            result--;

            ShowResult("--" + firstTime + " = " + result);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ByteButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Time firstTime = ReadFirstTime();

            byte hours = (byte)firstTime;

            ShowResult("Явное приведение к byte: " + hours);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void BoolButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Time firstTime = ReadFirstTime();
            Time secondTime = ReadSecondTime();

            bool firstResult = firstTime;
            bool secondResult = secondTime;

            ShowResult(
                "Первое время: " + firstTime + " -> " + firstResult + Environment.NewLine +
                "Второе время: " + secondTime + " -> " + secondResult);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void TimePlusUIntButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Time firstTime = ReadFirstTime();
            uint minutes = ReadUnsignedMinutes();

            Time result = firstTime + minutes;

            ShowResult(firstTime + " + " + minutes + " минут = " + result);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void UIntPlusTimeButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Time firstTime = ReadFirstTime();
            uint minutes = ReadUnsignedMinutes();

            Time result = minutes + firstTime;

            ShowResult(minutes + " минут + " + firstTime + " = " + result);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void TimePlusTimeButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Time firstTime = ReadFirstTime();
            Time secondTime = ReadSecondTime();

            Time result = firstTime + secondTime;

            ShowResult(firstTime + " + " + secondTime + " = " + result);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        FirstHoursTextBox.Clear();
        FirstMinutesTextBox.Clear();
        SecondHoursTextBox.Clear();
        SecondMinutesTextBox.Clear();
        AddMinutesTextBox.Clear();
        ResultTextBox.Clear();
    }

    private Time ReadFirstTime()
    {
        byte hours = ReadHours(FirstHoursTextBox.Text);
        byte minutes = ReadMinutes(FirstMinutesTextBox.Text);

        return new Time(hours, minutes);
    }

    private Time ReadSecondTime()
    {
        byte hours = ReadHours(SecondHoursTextBox.Text);
        byte minutes = ReadMinutes(SecondMinutesTextBox.Text);

        return new Time(hours, minutes);
    }

    private byte ReadHours(string text)
    {
        byte value;
        bool isParsed = byte.TryParse(text, out value);

        if (!isParsed || value > 23)
        {
            throw new ArgumentException("Введите корректные часы от 0 до 23.");
        }

        return value;
    }

    private byte ReadMinutes(string text)
    {
        byte value;
        bool isParsed = byte.TryParse(text, out value);

        if (!isParsed || value > 59)
        {
            throw new ArgumentException("Введите корректные минуты от 0 до 59.");
        }

        return value;
    }

    private uint ReadUnsignedMinutes()
    {
        uint value;
        bool isParsed = uint.TryParse(AddMinutesTextBox.Text, out value);

        if (!isParsed)
        {
            throw new ArgumentException("Введите корректное неотрицательное количество минут.");
        }

        return value;
    }

    private void ShowResult(string text)
    {
        ResultTextBox.Text = text;
    }

    private void ShowError(string message)
    {
        MessageBox.Show(
            message,
            "Ошибка",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}