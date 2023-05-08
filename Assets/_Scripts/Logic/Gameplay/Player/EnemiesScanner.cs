using Logic.Gameplay.Damage;
using UnityEngine;

namespace Logic.Gameplay.Player
{
    public class EnemiesScanner
    {
        private const int SPHERE_RADIUS = 20;

        private Transform center;
        private int? enemyLayer;

        private EntityHealth target;

        private int EnemyLayer
        {
            get
            {
                if (enemyLayer == null)
                    enemyLayer = LayerMask.GetMask("Enemy");

                return enemyLayer.Value;
            }
        }

        public Transform ClosestEnemy { get; private set; }

        public EnemiesScanner(Transform center)
        {
            this.center = center;
        }

        public void Scan()
        {

            if (target != null && target.IsDead)
            {
                ClosestEnemy = null;
                target = null;
            }

            Collider[] hitColliders = new Collider[5];

            int numColliders = Physics.OverlapSphereNonAlloc(center.position, SPHERE_RADIUS, hitColliders, EnemyLayer);

            float minDistance = int.MaxValue;
            Transform closestTarget = null;
            EntityHealth entityHealth = null;
            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i] == null)
                    continue;

                float distanceToTatget = (center.position - hitColliders[i].transform.position).sqrMagnitude;
                if (distanceToTatget < minDistance && hitColliders[i].TryGetComponent(out entityHealth) && !entityHealth.IsDead)
                {
                    minDistance = distanceToTatget;
                    closestTarget = hitColliders[i].transform;
                }
            }
            target = entityHealth ? entityHealth : target;
            ClosestEnemy = closestTarget ? closestTarget : ClosestEnemy;
        }
    }
}