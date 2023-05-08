using UnityEngine;

namespace Logic.Gameplay.Animations
{
    public class HumanoidAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator animator;

        private readonly int WALK_HASH = Animator.StringToHash("Walk");
        private readonly int DIE_HASH = Animator.StringToHash("Die");
        private readonly int ALIVE_HASH = Animator.StringToHash("Alive");

        public void PlayWalk() =>
            animator.SetBool(WALK_HASH, true);

        public void StopWalk() =>
            animator.SetBool(WALK_HASH, false);

        public void PlayDie() =>
            animator.SetTrigger(DIE_HASH);

        public void PlayAlive() =>
            animator.SetTrigger(ALIVE_HASH);
    }
}