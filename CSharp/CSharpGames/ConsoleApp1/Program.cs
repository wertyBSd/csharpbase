using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10; i++)
        {
            sb.AppendLine($"i={i.ToString()}");
        }
        Console.WriteLine( sb.ToString() );
        Console.ReadKey();
    }
}