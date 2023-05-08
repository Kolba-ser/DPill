using StaticData.Guns;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Logic.Gameplay.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected Transform shotPoint;

        protected WeaponInfo weaponInfo;

        protected IDisposable shooting;

        public virtual void SetParameters(WeaponInfo weaponInfo)
        {
            this.weaponInfo = weaponInfo;
        }

        public void StartShoot()
        {
            StopShoot();
            shooting = Observable
                .Interval((1 / weaponInfo.Rate).sec())
                .Subscribe(_ => Shoot());
        }

        public void StopShoot() => 
            shooting?.Dispose();

        protected abstract void Shoot();
    }
}