using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Pult pult = new Pult();
        TV tv = new TV();
        pult.SetCommand(new TVOnCommand(tv));
        pult.PressButton();
        pult.PressUndo();

        Console.Read();
    }
}

abstract class Command
{
    public abstract void Execute();
    public abstract void Undo();
}

// Receiver - Получатель
class TV
{
    public void On()
    {
        Console.WriteLine("Телевизор включен!");
    }

    public void Off()
    {
        Console.WriteLine("Телевизор выключен...");
    }
}

class TVOnCommand : Command
{
    TV tv;
    public TVOnCommand(TV tvSet)
    {
        tv = tvSet;
    }
    public override void Execute()
    {
        tv.On();
    }
    public override void Undo()
    {
        tv.Off();
    }
}

// Invoker - инициатор
class Pult
{
    Command command;

    public Pult() { }

    public void SetCommand(Command com)
    {
        command = com;
    }

    public void PressButton()
    {
        command.Execute();
    }
    public void PressUndo()
    {
        command.Undo();
    }
}