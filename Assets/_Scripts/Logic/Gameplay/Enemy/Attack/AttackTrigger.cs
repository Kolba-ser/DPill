using Logic.Gameplay.Damage;
using Logic.Gameplay.Player;
using UnityEngine;

namespace Logic.Gameplay.Enemy.Attack
{
    [RequireComponent(typeof(SphereCollider))]
    public class AttackTrigger : MonoBehaviour
    {
        private bool withinReach;

        private EntityHealth playerHealth;

        public bool WithinReach => withinReach && playerHealth != null && !playerHealth.IsDead; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerComponents player))
            {
                playerHealth = player.Health;
                withinReach = true;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerComponents>() != null)
            {
                playerHealth = null;
                withinReach = false;
            }
        }
    }
}