using Infrastructure.Initializers;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Providers.Assets;
using Logic.Gameplay.Weapons;
using StaticData.Guns;
using UnityEngine;
using UnityEngine.Assertions;

namespace Infrastructure.Services.Guns
{
    public class WeaponSelectionService : IWeaponSelectionService
    {
        private readonly AllServices allServices;
        private readonly WeaponsInfoContainer weaponsInfo;

        public WeaponSelectionService(IAssetsProvider assetsProvider, AllServices allServices)
        {
            this.allServices = allServices;
            weaponsInfo = assetsProvider.Load<WeaponsInfoContainer>(AssetsPath.WEAPONS_PATH);
        }

        public WeaponBase SpawnActiveGun()
        {
            var progressService = allServices.GetSingle<IPersistentProgressService>();

            string activeGunName = progressService.Progress.GunName ==
                string.Empty
                ? "EMPTY"
                : progressService.Progress.GunName;

            WeaponBase weaponPrefab = null;
            WeaponInfo weaponInfo = default;

            foreach (var pair in weaponsInfo.WeaponsInfo)
            {
                if (pair.Key == activeGunName)
                {
                    weaponPrefab = pair.Value.WeaponPrefab;
                    weaponInfo = pair.Value;
                    break;
                }
            }

            Assert.IsNotNull(weaponPrefab, $"Not found gun prefab ({activeGunName}) in WeaponsInfoContainer");
            var initializer = new WeaponInitializer(allServices, weaponInfo);
            var factory = new InitializableAssetFactory<WeaponBase>(weaponPrefab, null, initializer.Initialize, false, false);

            return factory.CreateAsset();
        }
    }
}