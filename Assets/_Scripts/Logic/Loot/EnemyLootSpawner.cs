using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Logic.Gameplay.Damage;
using UnityEngine;

namespace Logic
{
    public class EnemyLootSpawner : MonoBehaviour, IDependOnDeath
    {
        [SerializeField, Range(1, 5)] private int maxAmount;
        [SerializeField] private Transform center;
        [SerializeField] private float width;

        public void OnDeath()
        {
            SpawnLoot();
        }

        private void SpawnLoot()
        {
            var gameFactory = AllServices.Container.GetSingle<IGameFactory>();
            Currency lootType = GetLootType();
            int amount = Random.Range(0, maxAmount);

            for (int i = 0; i < amount; i++)
            {
                Loot loot = gameFactory.CreateAsset<Loot>(lootType.ToString(), pathPostfix: "_Loot");
                loot.transform.position = GetSpawnPosition();
            }

        }

        private Vector3 GetSpawnPosition() =>
            new Vector3
                (
                    Random.Range(center.position.x - (width / 2), center.position.x + (width / 2)),
                    center.position.y,
                    Random.Range(center.position.z - (width / 2), center.position.z + (width / 2))
                );

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireCube(center.position, new Vector3(width, 0.1f, width));
        }
#endif

        private Currency GetLootType() =>
            (Currency)Random.Range(0, 2);
    }
}