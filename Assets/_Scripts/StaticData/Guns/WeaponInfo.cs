using Logic.Gameplay.Weapons;
using Logic.Gameplay.Weapons.Projectiles;
using System;
using UnityEngine;

namespace StaticData.Guns
{
    [Serializable]
    public struct WeaponInfo
    {
        public WeaponProjectile ProjectilePrefab;
        public WeaponBase WeaponPrefab;

        public int Damage;
        public float Rate;
        public float ProjectileSpeed;
    }



}