using CountMarbles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static void Main(string[] args)
    {
        //setup our DI
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddScoped<IColorWeightService, ColorWeightService>()
            .AddScoped<CountMarbles.CountMarbles>()
            .BuildServiceProvider();

        //configure console logging
        serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

        var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        logger.LogDebug("Starting application");

        //do the actual work here
        var countMarbles = serviceProvider.GetService<CountMarbles.CountMarbles>();
        
        var count = countMarbles.Counter(new[] { "red", "white", "black", "red", "red" });
        foreach (var color in count)
        {
            Console.WriteLine($"{color.Key} : {color.Value}");
        }
    }

}