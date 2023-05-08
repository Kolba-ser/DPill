using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Logic.Gameplay.Weapons;
using StaticData.Guns;

namespace Infrastructure.Initializers
{
    internal class WeaponInitializer : GameEntityInitializer<WeaponBase>
    {
        private readonly WeaponInfo weaponInfo;

        public WeaponInitializer(AllServices allServices, WeaponInfo weaponInfo) : base(allServices)
        {
            this.weaponInfo = weaponInfo;
        }

        public override WeaponBase CreateAndInitialize()
        {
            WeaponBase prefab = weaponInfo.WeaponPrefab;
            var factory = new InitializableAssetFactory<WeaponBase>(prefab, null, Initialize, false, false);
            return factory.CreateAsset();
        }

        public override void Initialize(WeaponBase entity) =>
            entity.SetParameters(weaponInfo);
    }
}