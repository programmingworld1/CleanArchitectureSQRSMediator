namespace Domain.ValueObjects
{
    public class Duration
    {
        public int TotalSeconds { get; }

        private Duration() { }

        public Duration(int seconds)
        {
            if (seconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(seconds), "Duration must be positive.");
            
            if (seconds > 7200) // Max 2 uur
                throw new ArgumentOutOfRangeException(nameof(seconds), "Duration cannot exceed 2 hours.");

            TotalSeconds = seconds;
        }
    }
}