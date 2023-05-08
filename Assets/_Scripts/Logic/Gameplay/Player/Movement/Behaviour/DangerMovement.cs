using Infrastructure.Services.Input;
using Logic.Gameplay.Animations;
using UnityEngine;

namespace Logic.Gameplay.Player.Movement.Behaviour
{
    public class DangerMovement : MovementBehaviour
    {
        private const int SPHERE_RADIUS = 20;

        public readonly IInputService inputService;
        private readonly float movementSpeed;
        private readonly float rotationSpeed;
        private CharacterController character;
        private EnemiesScanner scanner;


        public DangerMovement(IInputService inputService, float movementSpeed, float rotationSpeed, EnemiesScanner scanner)
        {
            this.inputService = inputService;
            this.movementSpeed = movementSpeed;
            this.rotationSpeed = rotationSpeed;
            this.scanner = scanner;
        }

        public override void Move(CharacterController controller, HumanoidAnimator animator)
        {
            Vector3 movementVector = Vector3.zero;

            if (!character)
                character = controller;

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
            if (movementVector != Vector3.zero || scanner.ClosestEnemy)
                Rotate(controller, movementVector);

            movementVector += Physics.gravity;

            controller.Move(movementSpeed * movementVector * Time.deltaTime);
        }

        private void Rotate(CharacterController controller, Vector3 movementVector)
        {
            Quaternion rotation = Quaternion.LookRotation(RotationVector(movementVector), Vector3.up);
            rotation.x = 0;
            rotation.z = 0;
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        private Vector3 RotationVector(Vector3 movementVector) =>
            scanner.ClosestEnemy
                ? (scanner.ClosestEnemy.position - character.transform.position).normalized
                : movementVector;
    }
}