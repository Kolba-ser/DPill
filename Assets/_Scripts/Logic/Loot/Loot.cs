using UnityEngine;

namespace Logic
{
    public class Loot : MonoBehaviour, ILoot
    {
        [SerializeField] private Currency lootType;
        [SerializeField] private int value;

        public Currency LootType => lootType;

        public int Value => value;

        public bool IsCollected { get; private set; }

        public void OnDrop()
        {
            IsCollected = false;
        }

        public void OnPickUp()
        {
            IsCollected = true;
        }
    }
}