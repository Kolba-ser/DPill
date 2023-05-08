using Logic.Level;
using Logic.Level.Spawners;
using UnityEngine;

namespace Assets._Scripts.Logic.Level.Data
{
    public class LocationMetaData : MonoBehaviour
    {


        [field: SerializeField]
        public Transform PlayerSpawnPoint { get; private set; }

        [field: SerializeField]
        public EnemiesSpawner Spawner { get; private set; }


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            if (PlayerSpawnPoint != null)
                Gizmos.DrawSphere(PlayerSpawnPoint.position, 1f);
        }

#endif
    }
}