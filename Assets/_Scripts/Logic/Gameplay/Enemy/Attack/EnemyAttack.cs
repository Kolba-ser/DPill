using Logic.Gameplay.Animations.Enemy;
using Logic.Gameplay.Damage;
using System.Linq;
using UnityEngine;

namespace Logic.Gameplay.Enemy.Attack
{
    public class EnemyAttack : MonoBehaviour, IDependOnDeath
    {
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private AttackTrigger attackTrigger;
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float cleavege = 0.5f;
        [SerializeField] private float effectiveDistance;
        [SerializeField] private int damage = 10;

        private float currentAttackCooldown;

        private bool isAttacking;
        private int layerMask;
        private Collider[] hits = new Collider[1];
        private bool attackIsActive;

        private void Awake()
        {
            EnableAttack();
            layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
            {
                StartAttack();
            }
        }

        public void OnDeath() =>
            DisableAttack();

        public void EnableAttack() =>
            attackIsActive = true;

        public void DisableAttack() =>
            attackIsActive = false;

        private void OnAttackStart()
        {
            if (Hit(out Collider hit) && hit.transform.TryGetComponent(out EntityHealth health))
            {
                DrawExtensions.DrawRay(hit.transform.position, cleavege, 2);
                health.Decrease(damage);
            }
        }

        private void OnAttackEnd()
        {
            currentAttackCooldown = attackCooldown;
            isAttacking = false;
            enemyMovement.IsActive = true;
        }

        private void StartAttack()
        {
            enemyAnimator.PlayAttack();
            isAttacking = true;
            enemyMovement.IsActive = false;
        }

        private bool CanAttack() =>
            attackIsActive
            && attackTrigger.WithinReach
            && !isAttacking
            && CooldownIsUp();

        private bool CooldownIsUp() =>
            currentAttackCooldown <= 0;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                currentAttackCooldown -= Time.deltaTime;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), cleavege, hits, layerMask);
            DrawExtensions.DrawRay(StartPoint(), cleavege, 2);
            hit = hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                transform.forward * effectiveDistance;
        }
    }
}