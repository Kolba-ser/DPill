using Logic.Gameplay.Damage;
using UnityEngine;

namespace Logic.Gameplay.Player.Attack
{
    public class Spotter : MonoBehaviour, IDependOnDeath
    {
        [SerializeField] private float spotSpeed;
        [SerializeField] private Transform shotPoint;
        [SerializeField] private PlayerComponents playerComponents;

        private bool isActive = true;

        public void OnDeath() => 
            isActive = false;

        private void Update()
        {
            if (!playerComponents.Scanner.ClosestEnemy || !isActive)
                return;

            Vector3 direction = (playerComponents.Scanner.ClosestEnemy.position - shotPoint.transform.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction);
            rotation.x = 0;
            rotation.z = 0;
            shotPoint.transform.rotation = Quaternion.Lerp(shotPoint.transform.rotation, rotation, spotSpeed * Time.deltaTime);
        }
    }
}