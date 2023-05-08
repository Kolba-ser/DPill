using Infrastructure.Services.Progress;
using System;

namespace Infrastructure.Services.Currency
{
    public class MoneyService : CurrencyService, IMoneyService
    {
        private IPersistentProgressService persistentProgress;

        public MoneyService(IPersistentProgressService persistentProgress)
        {
            this.persistentProgress = persistentProgress;
        }

        public event Action<int> OnValueChanged;

        public override int CurrentValue => persistentProgress.Progress.Money;

        public override void Add(int value)
        {
            if (value < 0)
                return;

            persistentProgress.Progress.Money += value;
            OnValueChanged?.Invoke(CurrentValue);
        }

        public override bool TryTake(int value)
        {
            if (CurrentValue >= value)
            {
                persistentProgress.Progress.Money -= value;
                OnValueChanged?.Invoke(CurrentValue);
            }

            return CurrentValue >= value;
        }
    }
}