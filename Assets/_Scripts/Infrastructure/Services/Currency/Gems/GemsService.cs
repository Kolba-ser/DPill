using Infrastructure.Services.Progress;
using System;

namespace Infrastructure.Services.Currency
{
    public class GemsService : CurrencyService, IGemsService
    {
        private IPersistentProgressService persistentProgress;

        public GemsService(IPersistentProgressService persistentProgress)
        {
            this.persistentProgress = persistentProgress;
        }

        public event Action<int> OnValueChanged;

        public override int CurrentValue => persistentProgress.Progress.Gems;

        public override void Add(int value)
        {
            if (value < 0)
                return;

            persistentProgress.Progress.Gems += value;
            OnValueChanged?.Invoke(CurrentValue);
        }

        public override bool TryTake(int value)
        {
            if (CurrentValue >= value)
            {
                persistentProgress.Progress.Gems -= value;
                OnValueChanged?.Invoke(CurrentValue);
            }

            return CurrentValue >= value;
        }
    }
}