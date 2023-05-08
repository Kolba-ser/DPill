namespace Infrastructure.Services.Currency
{
    public abstract class CurrencyService
    {
        public abstract int CurrentValue { get; }

        public abstract void Add(int value);
        public abstract bool TryTake(int value);
    }
}