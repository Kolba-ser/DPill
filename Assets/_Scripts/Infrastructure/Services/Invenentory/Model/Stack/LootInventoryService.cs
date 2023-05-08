
using Infrastructure.Services.Player;
using Logic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Invenentory.Model.StackModel
{
    public class LootInventoryService : InventoryBase<ILoot>, ILootInventoryService
    {
        private const int MAX_CELLS_AMOUNT = 24;

        private readonly Stack<IInventoryCell<ILoot>> stackItems;

        public override IReadOnlyCollection<IInventoryCell<ILoot>> Collection => stackItems;

        public event Action OnItemAdded;

        public event Action OnItemRemoved;

        public int CellsCount => MAX_CELLS_AMOUNT;

        public LootInventoryService()
        {
            stackItems = new Stack<IInventoryCell<ILoot>>();
        }

        public bool TryPush(ILoot loot)
        {
            if (stackItems.Count == MAX_CELLS_AMOUNT || loot == null)
                return false;

            stackItems.Push(CreateCell(loot));
            OnItemAdded?.Invoke();
            loot.OnPickUp();
            return true;
        }

        public bool TryPop(out ILoot loot)
        {
            loot = null;

            if (stackItems.Count == 0)
                return false;

            var cell = stackItems.Pop();
            loot = cell.Take();
            OnItemRemoved?.Invoke();
            loot.OnDrop();
            return true;
        }

        public ILoot Peek()
        {
            if (stackItems.Count == 0)
                return null;

            return stackItems.Peek().StoredEntity;
        }

        public void Clear()
        {
            // Not his responsibility
            foreach (var cell in Collection)
            {
                if (!cell.IsEmpty)
                    GameObject.Destroy(cell.Take().transform.gameObject);
            }

            stackItems.Clear();
        }
        private IInventoryCell<ILoot> CreateCell(ILoot loot)
        {
            var cell = new InventoryCell<ILoot>();
            cell.Put(loot);
            return cell;
        }
    }
}