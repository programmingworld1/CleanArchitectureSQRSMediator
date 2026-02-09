namespace Domain.ValueObjects
{
    public record Year
    {
        public int Value { get; }

        public Year(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Year must be positive.");
            
            if (value > DateTime.UtcNow.Year) 
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Year cannot be in the future.");

            Value = value;
        }
    }
}