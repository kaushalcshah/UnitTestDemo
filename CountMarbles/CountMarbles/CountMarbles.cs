using Microsoft.Extensions.Logging;

namespace CountMarbles
{
    public class CountMarbles
    {
        private IColorWeightService _colorWeightService;
        private ILogger _logger;
        
        public CountMarbles(IColorWeightService colorWeightService, ILogger<CountMarbles> logger)
        {
            _colorWeightService = colorWeightService;
            _logger = logger;
        }

        public Dictionary<string, int> Counter(string[] marbles)
        {
            if (marbles == null)
            {
                throw new ArgumentNullException(nameof(marbles));
            }
            var counter = new Dictionary<string, int>();
            foreach (var marble in marbles)
            {
                if (string.IsNullOrWhiteSpace(marble)) continue;
                var weight = _colorWeightService.GetColorWeight(marble);
                if (counter.ContainsKey(marble))
                {
                    _logger.LogDebug($"Updated {marble} marbel with adding weight {weight}");
                    counter[marble] = counter[marble] + weight;
                }
                else
                {
                    _logger.LogDebug($"Added {marble} marbel with weight {weight}");
                    counter[marble] = weight;
                }
            }
            return counter;
        }
    }
}
