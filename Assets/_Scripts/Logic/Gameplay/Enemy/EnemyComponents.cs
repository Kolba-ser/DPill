using Assets._Scripts.Logic.Gameplay.Enemy;
using Logic.Gameplay.Animations.Enemy;
using Logic.Gameplay.Damage;
using UnityEngine;

namespace Logic.Gameplay.Enemy
{
    public class EnemyComponents : MonoBehaviour
    {
        [field: SerializeField] 
        public EnemyMovement Movement { get; private set; }

        [field: SerializeField]
        public EnemyAnimator Animator { get; private set; }

        [field: SerializeField]
        public EntityHealth Health { get; private set; }

        [field: SerializeField]
        public EnemyFollower Follower { get; private set; }

    }
}