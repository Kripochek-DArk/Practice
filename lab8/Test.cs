using System;
internal class Test
{
    private int _id;
    private string _subject;
    private string _topic;
    private int _questionCount;
    private double _difficulty;
    private bool _isAvailable;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Subject
    {
        get => _subject;
        set => _subject = value;
    }

    public string Topic
    {
        get => _topic;
        set => _topic = value;
    }

    public int QuestionCount
    {
        get => _questionCount;
        set => _questionCount = value;
    }

    public double Difficulty
    {
        get => _difficulty;
        set => _difficulty = value;
    }

    public bool IsAvailable
    {
        get => _isAvailable;
        set => _isAvailable = value;
    }

    public Test()
    {
        _subject = string.Empty;
        _topic = string.Empty;
    }

    public Test(int id, string subject, string topic, int questionCount, double difficulty, bool isAvailable)
    {
        _id = id;
        _subject = subject;
        _topic = topic;
        _questionCount = questionCount;
        _difficulty = difficulty;
        _isAvailable = isAvailable;
    }

    public override string ToString()
    {
        string status = _isAvailable ? "доступен" : "недоступен";

        return $"ID: {_id}, Предмет: {_subject}, Тема: {_topic}, " +
               $"Вопросов: {_questionCount}, Сложность: {_difficulty:F1}, Статус: {status}";
    }
}