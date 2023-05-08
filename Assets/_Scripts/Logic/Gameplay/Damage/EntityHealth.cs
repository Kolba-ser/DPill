using System;
using UnityEngine;

namespace Logic.Gameplay.Damage
{
    public class EntityHealth : MonoBehaviour
    {
        [SerializeField] private int amount;
        public int Amount => amount;

        public bool IsDead => Amount <= 0;

        public event Action<int> OnHealthChanged;

        public event Action<EntityHealth> OnDeath;

        public void Increase(int value)
        {
            if (value < 0)
                return;

            amount += value;
            OnHealthChanged?.Invoke(amount);
        }

        public void Decrease(int value)
        {
            if (value < 0 || amount == 0)
                return;

            if (value >= amount)
            {
                value = amount;
                Notify();
                OnDeath?.Invoke(this);
            }

            amount -= value;
            OnHealthChanged?.Invoke(amount);
        }

        private void Notify()
        {
            var components = GetComponentsInChildren<IDependOnDeath>();
            
            foreach (var component in components)
            {
                component.OnDeath();
            }
        }
    }
}