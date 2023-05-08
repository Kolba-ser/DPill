using Infrastructure.Services.Input;
using Logic.Gameplay.Animations;
using UnityEngine;

namespace Logic.Gameplay.Player.Movement.Behaviour
{
    public class SafeMovement : MovementBehaviour
    {
        public IInputService inputService;
        private readonly float movementSpeed;
        private readonly float rotationSpeed;

        public SafeMovement(IInputService inputService, float movementSpeed, float rotationSpeed)
        {
            this.inputService = inputService;
            this.movementSpeed = movementSpeed;
            this.rotationSpeed = rotationSpeed;
        }

        public override void Move(CharacterController controller, HumanoidAnimator animator)
        {
            Vector3 movementVector = Vector3.zero;

            if (inputService.Axis.sqrMagnitude > Mathf.Epsilon)
            {
                movementVector.x = inputService.Axis.x;
                movementVector.y = 0;
                movementVector.z = inputService.Axis.y;
                movementVector.Normalize();

                animator.PlayWalk();
            }
            else
            {
                animator.StopWalk();
            }

            if (movementVector != Vector3.zero)
                Rotate(controller, movementVector);


            movementVector += Physics.gravity;

            controller.Move(movementSpeed * movementVector * Time.deltaTime);
        }

        private void Rotate(CharacterController controller, Vector3 movementVector)
        {
            Quaternion rotation = Quaternion.LookRotation(movementVector, Vector3.up);

            rotation.x = 0;
            rotation.z = 0;
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}