using Logic.Gameplay.Damage;
using UniRx;
using UnityEngine;

namespace Logic.Gameplay.Animations.Enemy
{
    public class DeathAnimator : MonoBehaviour, IDependOnDeath
    {
        [SerializeField] private HumanoidAnimator animator;
        [SerializeField] private bool destroyAfterDeath;
        [SerializeField] private float destroyDelay;

        public void OnDeath() =>
            PlayDeath();

        private void PlayDeath()
        {
            animator.PlayDie();
            if (destroyAfterDeath)
            {
                Observable.Timer(destroyDelay.sec())
                    .Subscribe(_ =>Destroy(gameObject));
            }
        }
    }
}