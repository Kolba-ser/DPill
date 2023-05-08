using Logic.Gameplay.Damage;
using Logic.Gameplay.Invenentory.Model;
using UnityEngine;

namespace Logic.Gameplay.Player
{
    public class PlayerComponents : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerMovement Movement { get; private set; }

        [field: SerializeField]
        public PlayerZoneTrigger ZoneTrigger { get; private set; }

        [field: SerializeField]
        public PlayerAttack PlayerAttack { get; private set; }

        [field: SerializeField]
        public EntityHealth Health { get; private set; }

        [field: SerializeField]
        public LootInventoryPresenter Inventory { get; private set; }

        [field: SerializeField]
        public PlayerLootCollector LootCollector { get; private set; }

        private EnemiesScanner scanner;

        public EnemiesScanner Scanner
        {
            get 
            {
                if(scanner == null)
                {
                    scanner = new EnemiesScanner(Movement.transform);
                }
                return scanner;
            }
        }
}
}