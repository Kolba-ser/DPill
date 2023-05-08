using Infrastructure.Services.Factory;
using Logic.Gameplay.Weapons.Projectiles;
using StaticData.Guns;
using System;
using UnityEngine;

namespace Logic.Gameplay.Weapons
{
    public class Pistol : WeaponBase
    {
        private IAssetFactory<WeaponProjectile> projectileFactory;

        public override void SetParameters(WeaponInfo weaponInfo)
        {
            base.SetParameters(weaponInfo);
            CreateFactory(weaponInfo);
        }

        private void CreateFactory(WeaponInfo weaponInfo) => 
            projectileFactory = new AssetFactory<WeaponProjectile>(weaponInfo.ProjectilePrefab, null, false);

        protected override void Shoot()
        {
            var projectile = projectileFactory.CreateAsset();
            projectile.transform.position = shotPoint.position;
            projectile.transform.forward = shotPoint.forward;
            projectile.Initialize(weaponInfo.ProjectileSpeed, weaponInfo.Damage, shotPoint.transform.forward);
        }
    }
}