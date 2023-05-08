using Infrastructure.Services;
using Infrastructure.Services.Player;
using Logic.Gameplay.Player;
using Logic.Level.Spawners;
using UnityEngine;

namespace Logic.Level
{
    public class EnemyFightTrigger : MonoBehaviour
    {
        [SerializeField] private EnemiesSpawner spawner;
        private IPersistentPlayerService persistentPlayer;

        private void Start()
        {
            persistentPlayer = AllServices.Container.GetSingle<IPersistentPlayerService>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<PlayerComponents>())
                StartFight();
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.GetComponent<PlayerComponents>())
                StopFight();
        }

        private void StartFight()
        {
            Transform player = persistentPlayer.Player.Movement.transform;

            foreach (var spawnedEnemy in spawner.SpawnedEnemies)
            {
                if(spawnedEnemy != null)
                    spawnedEnemy.Movement.StartFollow(player);
            }
        }

        private void StopFight()
        {
            foreach (var spawnedEnemy in spawner.SpawnedEnemies)
            {
                if (spawnedEnemy != null)
                    spawnedEnemy.Movement.StopFollow();
            }
        }
    }
}