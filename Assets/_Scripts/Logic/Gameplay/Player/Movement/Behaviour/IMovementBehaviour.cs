using Logic.Gameplay.Animations;
using UnityEngine;

namespace Logic.Gameplay.Player.Movement.Behaviour
{
    public interface IMovementBehaviour
    {
        public void Move(CharacterController controller, HumanoidAnimator animator);
    }
}