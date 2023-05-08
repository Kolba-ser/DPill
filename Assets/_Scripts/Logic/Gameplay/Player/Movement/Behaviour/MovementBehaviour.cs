using Logic.Gameplay.Animations;
using UnityEngine;

namespace Logic.Gameplay.Player.Movement.Behaviour
{
    public abstract class MovementBehaviour : IMovementBehaviour
    {
        public abstract void Move(CharacterController controller, HumanoidAnimator animator);
    }
}