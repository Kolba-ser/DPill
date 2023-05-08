using UnityEngine;

namespace Logic
{
    public interface ILoot
    {
        public Currency LootType { get; }

        public Transform transform { get; } 

        public int Value { get; }
        
        public bool IsCollected { get; }

        public void OnPickUp();
        public void OnDrop();
    }
}