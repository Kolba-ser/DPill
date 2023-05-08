using System;
using UnityEngine;

namespace Logic.Gameplay.Animations.Enemy
{
    public class EnemyAnimator : HumanoidAnimator
    {
        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

        public event Action OnAttacked;
        public event Action OnAttackEnd;

        public bool Attacking { get; private set; }

        public void PlayAttack()
        {
            if (!Attacking)
                animator.SetTrigger(ATTACK_HASH);
        }

        private void OnAttackStart()
        {
            Attacking = true;
            OnAttacked.Invoke();
        }

        private void OnAttackStop()
        {
            Attacking = false;
            OnAttackEnd.Invoke();
        }
    }
}