class Message
{
    public string Text { get; } // текст сообщения
    public Message(string text)
    {
        Text = text;
    }
}
class MessageWithoutArgs
{
    public string Text { get; set} // текст сообщения
    public MessageWithoutArgs()
    {
        Text = string.Empty;
    }
}
class EmailMessage : Message
{
    public EmailMessage(string text) : base(text) { }
}
// Добавляем ограничения на используемые классы
class Messenger<T> where T : Message
{
    public void SendMessage(T message)
    {
        Console.WriteLine($"Send message: {message.Text}");
    }
}

struct MessageStruct
{
    public string Text { get; } // текст сообщения
    public MessageStruct(string text)
    {
        Text = text;
    }
}
// Т - тип может являться только структурой
class MessengerStruct<T> where T : struct
{
    public void GetType(T message)
    {
        Console.WriteLine($"Get type message: {message.GetType()}");
    }
}
// Т - тип может являться только классом
class MessengerClass<T> where T : class
{
    public void GetType(T message)
    {
        Console.WriteLine($"Get type message: {message.GetType()}");
    }
}

// Пример где все типы должны иметь конструктор без параметров
class MessengerNew<T> where T : new()
{
    public void GetType(T message)
    {
        Console.WriteLine($"Get type message: {message.GetType()}");
    }
}

internal class Program
{
    // Добавляем ограниечение на тип входного параметра для функции
    // когда мы отправляем EmailMessage он неявно прводится к типу Message
    static void SendMessage<T>(T message) where T : Message
    {
        Console.WriteLine($"Send message: {message.Text}");
    }
    private static void Main(string[] args)
    {
        // Пример с ограничением на метод
        SendMessage(new Message("Hello World"));
        SendMessage(new EmailMessage("Bye World"));

        // Пример с ограничением на класс
        Messenger<Message> telegram = new Messenger<Message>();
        telegram.SendMessage(new Message("Hello World"));

        Messenger<EmailMessage> outlook = new Messenger<EmailMessage>();
        outlook.SendMessage(new EmailMessage("Bye World"));

        // Ограничение на то, что бы тип являлся труктурой
        MessengerStruct<MessageStruct> icq = new MessengerStruct<MessageStruct>();
        icq.GetType(new MessageStruct());

        // Ограничение на то, что бы тип являлся классом
        MessengerClass<EmailMessage> outlook2 = new MessengerClass<EmailMessage>();
        outlook2.GetType(new EmailMessage("Bye World"));

        // Класс который используется, должен иметь конструктор без параметров, первый пример работать не будет
        // MessengerNew<EmailMessage> outlook3 = new MessengerNew<EmailMessage>();
        MessengerNew<MessageWithoutArgs> outlook3 = new MessengerNew<MessageWithoutArgs>();
        outlook3.GetType(new MessageWithoutArgs() { Text = "Hello World" });
    }
}