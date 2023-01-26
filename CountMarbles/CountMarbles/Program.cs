internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var countMarbles = new CountMarbles.CountMarbles();
        var count = countMarbles.Counter(new[] { "red", "white", "black", "red", "red" });
        foreach (var color in count)
        {
            Console.WriteLine($"{color.Key} : {color.Value}");
        }
    }
}