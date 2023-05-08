using Infrastructure.Services.Interfaces;
using Logic.Gameplay.Weapons;

namespace Infrastructure.Services.Guns
{
    public interface IWeaponSelectionService : IService
    {
        public WeaponBase SpawnActiveGun();
    }
}