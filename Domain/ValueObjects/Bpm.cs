namespace Domain.ValueObjects
{
    public record Bpm
    {
        public int Value { get; }

        private Bpm() { }

        public Bpm(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "BPM must be positive.");

            Value = value;
        }
    }
}