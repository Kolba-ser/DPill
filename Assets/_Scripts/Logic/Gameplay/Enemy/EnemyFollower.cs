using Logic.Gameplay.Enemy;
using Logic.Gameplay.Player;
using UnityEngine;

namespace Assets._Scripts.Logic.Gameplay.Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyFollower : MonoBehaviour
    {
        [SerializeField] private EnemyMovement enemyMovement;

        private PlayerZoneTrigger zoneTrigger;
        private Transform playerTransform;
        
        public void TrackPlayer(PlayerZoneTrigger zoneTrigger, Transform playerTransform)
        {
            if (this.zoneTrigger)
                UnsubscribeFromPlayer();

            this.zoneTrigger = zoneTrigger;
            this.playerTransform = playerTransform;
            
            SubscribeToPlayer();
        }

        private void OnDisable() => 
            UnsubscribeFromPlayer();

        private void SubscribeToPlayer()
        {
            zoneTrigger.OnDanger += StartFollow;
            zoneTrigger.OnSafe += StopFollow;
        }
        private void UnsubscribeFromPlayer()
        {
            zoneTrigger.OnDanger -= StartFollow;
            zoneTrigger.OnSafe -= StopFollow;
        }

        private void StartFollow() => 
            enemyMovement.StartFollow(playerTransform);

        private void StopFollow() => 
            enemyMovement.StopFollow();
    }
}