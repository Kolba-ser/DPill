using Logic.Gameplay.Damage;
using Logic.Gameplay.Weapons;
using UnityEngine;

namespace Logic.Gameplay.Player
{
    [RequireComponent(typeof(PlayerZoneTrigger))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform weaponPoint;
        [SerializeField] private PlayerZoneTrigger trigger;
        [SerializeField] private PlayerComponents playerComponents;

        public bool IsAttacking { get; private set; }

        private WeaponBase activeWeapon;

        private void Awake()
        {
            trigger = trigger ?? GetComponent<PlayerZoneTrigger>();
            trigger.OnDanger += StartAttack;
            trigger.OnSafe += StopAttack;
        }

        public void SetWeapon(WeaponBase weaponBase)
        {
            activeWeapon = weaponBase;
            activeWeapon.transform.SetParent(weaponPoint);
            activeWeapon.transform.localPosition = Vector3.zero;
            activeWeapon.transform.eulerAngles = weaponPoint.transform.eulerAngles;
        }

        public void StartShoot()
        {
            activeWeapon?.StartShoot();
            IsAttacking = true;
        }

        public void StopShoot()
        {
            activeWeapon?.StopShoot();
            IsAttacking = false;
        }

        private void StartAttack()
        {
            StartShoot();
            ShowWeapon();
        }

        private void StopAttack()
        {
            StopShoot();
            HideWeapon();
        }

        private void ShowWeapon() =>
            activeWeapon?.gameObject.SetActive(true);

        private void HideWeapon() =>
            activeWeapon?.gameObject.SetActive(false);

    }
}