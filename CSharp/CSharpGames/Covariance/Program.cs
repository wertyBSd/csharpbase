

interface IMessenger<out T>
{
    T WriteMessage(string text);
}
class Message
{
    public string Text { get; set; }
    public Message(string text) => Text = text;
}
class EmailMessage : Message
{
    public EmailMessage(string text) : base(text) { }
}
class EmailMessenger : IMessenger<EmailMessage>
{
    public EmailMessage WriteMessage(string text)
    {
        return new EmailMessage($"Email: {text}");
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        IMessenger<Message> outlook = new EmailMessenger();
        Message message = outlook.WriteMessage("Hello World");
        Console.WriteLine(message.Text);    // Email: Hello World


        IMessenger<EmailMessage> emailClient = new EmailMessenger();
        IMessenger<Message> messenger = emailClient;
        Message emailMessage = messenger.WriteMessage("Hi!");
        Console.WriteLine(emailMessage.Text);    // Email: Hi!
    }
}