using Logic.Gameplay.Animations;
using Logic.Gameplay.Damage;
using Logic.Gameplay.Player.Movement.Behaviour;
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IDependOnDeath
{
    [SerializeField] private float movementSpeed = 4.0f;
    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField] private PlayerAnimator animator;

    [SerializeField] private CharacterController characterController;

    private IMovementBehaviour movementBehaviour;

    private bool isActive = true;

    public float MovementSpeed => movementSpeed;

    public float RotationSpeed => rotationSpeed;

    public Transform transform => characterController.transform;

    public void OnDeath() => 
        isActive = false;

    public void Initialize(Transform spawnPoint)
    {
        SetPosition(spawnPoint.position);
    }

    public void SetBehaviour(IMovementBehaviour behaviour) =>
        movementBehaviour = behaviour;

    private void Update()
    {
        if (!isActive)
            return;

        movementBehaviour?.Move(characterController, animator);
    }

    private void SetPosition(Vector3 position) =>
        characterController.transform.position = position;
}