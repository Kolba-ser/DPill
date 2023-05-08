using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Player;
using JetBrains.Annotations;
using Logic.Gameplay.Damage;
using Logic.Gameplay.Enemy;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;

namespace Logic.Level.Spawners
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField, Range(0, 20)] private int maxAmount;
        [SerializeField] private float spawnInterval;

        private List<EnemyComponents> spawnedEnemies;

        private int currentAmount;

        private IDisposable spawning;
        private IGameFactory gameFactory;

        public IReadOnlyCollection<EnemyComponents> SpawnedEnemies => spawnedEnemies;

        public void Initialize(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
            spawnedEnemies = new List<EnemyComponents>(maxAmount);
        }

        public void StartSpawn()
        {
            spawning?.Dispose();
            spawning = Observable.Interval(spawnInterval.sec())
                .Subscribe(_ =>
                {
                    if (currentAmount < maxAmount)
                        SpawnEnemy();
                });
        }

        private void SpawnEnemy()
        {
            Transform spawnPoint = spawnPoints.GetRandom();
            EnemyComponents enemy = gameFactory.CreateAsset<EnemyComponents>("Enemy");
            spawnedEnemies.Add(enemy);
            Initialize(spawnPoint, enemy);

            currentAmount++;
        }
        
        private void OnEnemyDeath(EntityHealth health)
        {
            foreach (var enemy in spawnedEnemies)
            {
                if(enemy.Health == health)
                {
                    enemy.Health.OnDeath -= OnEnemyDeath;
                    spawnedEnemies.Remove(enemy);
                    currentAmount--;
                    return;
                }
            }
        }

        private void Initialize(Transform spawnPoint, EnemyComponents enemy)
        {
            var persistentPlayer = AllServices.Container.GetSingle<IPersistentPlayerService>();
            enemy.Follower.TrackPlayer(persistentPlayer.Player.ZoneTrigger, persistentPlayer.Player.Movement.transform);
            enemy.Movement.Disable();
            enemy.Movement.SetPosition(spawnPoint.position);
            enemy.Movement.Enable();
            enemy.Health.OnDeath += OnEnemyDeath;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            foreach (var spawnPoint in spawnPoints)
                Gizmos.DrawSphere(spawnPoint.position, 1f);
        }

#endif
    }
}