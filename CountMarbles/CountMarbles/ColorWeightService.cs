using Microsoft.Extensions.Logging;

namespace CountMarbles
{
    public class ColorWeightService : IColorWeightService
    {
        private readonly Dictionary<string, int> colorWeight = new();
        private readonly int lowerLimit = 0;
        private readonly int upperLimit = 100;
        static readonly HttpClient client = new();
        private readonly ILogger _logger;

        public ColorWeightService(ILogger<ColorWeightService> logger)
        {
            _logger = logger;
        }

        public int GetColorWeight(string color)
        {
            if (colorWeight.ContainsKey(color))
            {
                return colorWeight[color];
            }
            else
            {
                var uri = $"https://www.random.org/integers/?num=1&min={lowerLimit}&max={upperLimit}&col=1&base=10&format=plain&rnd=new";
                var task = client.GetAsync(uri).Result;
                if (task.IsSuccessStatusCode)
                {
                    var number = int.Parse(Convert.ToString(task.Content.ReadAsStringAsync().Result));
                    _logger.LogDebug($"Assigned {color} marbel {number} weight");
                    colorWeight.Add(color, number);
                    return number;
                }
                var defaultWeight = 1;
                _logger.LogCritical($"Could not find weight for {color}, assigning default weight of {defaultWeight}."); 
                _logger.LogDebug($"Assigned {color} marbel {defaultWeight} weight");
                colorWeight.Add(color, defaultWeight);
                return defaultWeight;
            }
        }
    }
}
