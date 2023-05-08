using Logic.Gameplay.Animations;
using Logic.Gameplay.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Logic.Gameplay.Animations
{
    public class ZoneAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerZoneTrigger trigger;
        [SerializeField] private PlayerAnimator animator;

        private void Start()
        {
            trigger.OnDanger += animator.PlayUnderThreat;
            trigger.OnSafe += animator.StopUnderThreat;
        }

        private void OnDestroy()
        {
            trigger.OnDanger -= animator.PlayUnderThreat;
            trigger.OnSafe -= animator.StopUnderThreat;
        }
    }
}
