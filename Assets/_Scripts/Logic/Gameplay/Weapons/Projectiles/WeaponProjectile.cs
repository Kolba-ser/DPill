using Logic.Gameplay.Damage;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Logic.Gameplay.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class WeaponProjectile : MonoBehaviour
    {
        [SerializeField] private Collider collider;
        [SerializeField] private Rigidbody rigidbody;

        private float speed;
        private int damage;
        private Vector3 direction;
        private bool isInitialized;

        private void Awake()
        {
            collider = collider ?? GetComponent<Collider>();
            rigidbody = rigidbody ?? GetComponent<Rigidbody>();

            rigidbody.isKinematic = true;
        }

        public void Initialize(float speed, int damage, Vector3 direction)
        {
            this.speed = speed;
            this.damage = damage;
            this.direction = direction;
            isInitialized = true;
        }


        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out EntityHealth health))
            {
                health.Decrease(damage);
            }
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if(!isInitialized)
                return;
            Vector3 offset = direction * speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(rigidbody.position + offset);
        }
    }
}