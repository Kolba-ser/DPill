using UnityEngine;

namespace Logic.Gameplay.Animations
{
    public class PlayerAnimator : HumanoidAnimator
    {
        private readonly int DANGER_HASH = Animator.StringToHash("Danger");

        public void PlayUnderThreat() =>
            animator.SetBool(DANGER_HASH, true);

        public void StopUnderThreat() =>
            animator.SetBool(DANGER_HASH, false);
    }
}