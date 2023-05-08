using Infrastructure.Services.Invenentory.Model.StackModel;
using UnityEngine;

namespace Logic.Gameplay.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerLootCollector : MonoBehaviour
    {
        private ILootInventoryService lootInventory;

        public void Initialize(ILootInventoryService lootInventory)
        {
            this.lootInventory = lootInventory;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ILoot loot) && !loot.IsCollected)
                lootInventory.TryPush(loot);
        }
    }
}