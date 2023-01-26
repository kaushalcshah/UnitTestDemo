namespace CountMarbles
{
    public class CountMarbles
    {
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
                if (counter.ContainsKey(marble))
                {
                    counter[marble] = counter[marble] + 1;
                }
                else
                {
                    counter[marble] = 1;
                }
            }
            return counter;
        }
    }
}
