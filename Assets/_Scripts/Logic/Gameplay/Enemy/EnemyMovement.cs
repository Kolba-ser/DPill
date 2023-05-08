using Logic.Gameplay.Animations.Enemy;
using Logic.Gameplay.Damage;
using UnityEngine;

namespace Logic.Gameplay.Enemy
{
    public class EnemyMovement : MonoBehaviour, IDependOnDeath
    {
        [SerializeField] private float stopDistance = 1f;
        [SerializeField] private float movementSpeed = 1f;
        [SerializeField] private float rotationSpeed = 4f;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private CharacterController characterController;

        private Transform target;

        [HideInInspector]
        public bool IsActive = true;

        public Transform transform => characterController.transform;

        public void OnDeath()
        {
            Disable();
        }

        public void Enable()
        {
            this.enabled = true;
            IsActive = true;
            characterController.enabled = true;
        }

        public void Disable()
        {
            IsActive = false;
            this.enabled = false;
            characterController.enabled = false;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void StartFollow(Transform transform) =>
            target = transform;

        public void StopFollow() =>
            target = null;

        private void Update()
        {
            if (!IsActive)
                return;

            Vector3 movementVector = Vector3.zero;

            if (target && (target.position - transform.position).sqrMagnitude > stopDistance)
            {
                movementVector = target.position - transform.position;
                movementVector.Normalize();

                animator.PlayWalk();
            }
            else
            {
                animator.StopWalk();
            }

            if (target)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                if (direction != Vector3.zero)
                    Rotate(characterController, direction);
            }

            movementVector += Physics.gravity;
            characterController.Move(movementSpeed * movementVector * Time.deltaTime);
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