using Infrastructure.Services;
using Infrastructure.Services.Input;
using Logic.Gameplay.Damage;
using Logic.Gameplay.Player.Movement.Behaviour;
using UnityEngine;

namespace Logic.Gameplay.Player
{
    [RequireComponent(typeof(PlayerZoneTrigger))]
    public class PlayerBehaviourSwitcher : MonoBehaviour, IDependOnDeath
    {
        [SerializeField] private PlayerComponents playerComponents;

        private bool isActive = true;

        private void Awake()
        {
            playerComponents = playerComponents ?? GetComponent<PlayerComponents>();

            playerComponents.ZoneTrigger.OnDanger += SwitchToDanger;
            playerComponents.ZoneTrigger.OnSafe += SwitchToSafe;
        }

        private void OnDestroy()
        {
            playerComponents.ZoneTrigger.OnDanger -= SwitchToDanger;
            playerComponents.ZoneTrigger.OnSafe -= SwitchToSafe;
        }

        private void Update()
        {
            if (!isActive)
                return;

            playerComponents.Scanner.Scan();

            bool onUnderAttackAndDontShooting = 
                playerComponents.Scanner.ClosestEnemy 
                && !playerComponents.PlayerAttack.IsAttacking 
                && playerComponents.ZoneTrigger.IsUnderDanger;

            bool safeAndShoothing = 
                !playerComponents.Scanner.ClosestEnemy 
                && playerComponents.PlayerAttack.IsAttacking
                && playerComponents.ZoneTrigger.IsUnderDanger;

            if (safeAndShoothing)
            {
                playerComponents.PlayerAttack.StopShoot();
            }
            else if (onUnderAttackAndDontShooting)
            {
                playerComponents.PlayerAttack.StartShoot();
            }
        }
        public void OnDeath()
        {
            playerComponents.PlayerAttack.StopShoot();
            isActive = false;
        }

        private void SwitchToDanger()
        {
            var inputService = AllServices.Container.GetSingle<IInputService>();
            float rotationSpeed = playerComponents.Movement.RotationSpeed;
            float movementSpeed = playerComponents.Movement.MovementSpeed;
            EnemiesScanner scanner = playerComponents.Scanner;

            playerComponents.Movement
                .SetBehaviour(new DangerMovement
                    (
                        inputService,
                        movementSpeed,
                        rotationSpeed,
                        scanner
                    ));
        }

        private void SwitchToSafe()
        {
            var inputService = AllServices.Container.GetSingle<IInputService>();
            float rotationSpeed = playerComponents.Movement.RotationSpeed;
            float movementSpeed = playerComponents.Movement.MovementSpeed;

            playerComponents.Movement
                .SetBehaviour(new SafeMovement
                    (
                        inputService,
                        movementSpeed,
                        rotationSpeed
                    ));
        }

    }
}