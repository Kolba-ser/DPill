using Infrastructure.Services.Interfaces;
using System;

namespace Infrastructure.Services.Currency
{
    public interface IGemsService : IService
    {
        int CurrentValue
        {
            get;
        }

        public event Action<int> OnValueChanged;

        void Add(int value);
        bool TryTake(int value);
    }
}