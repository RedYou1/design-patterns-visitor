internal class Printer
{
	public static void Print(string name)
	{
		Console.WriteLine(name);
	}
}
public class Program
{
	public static void Main()
	{
		string name = "Jean-Christophe";
		Printer.Print(name);
	}
}