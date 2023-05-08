using Infrastructure.Services.Interfaces;
using Logic;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services.Invenentory.Model.StackModel
{
    public interface ILootInventoryService : IService
    {
        public int CellsCount
        {
            get;
        }

        public event Action OnItemAdded;

        public event Action OnItemRemoved;

        IReadOnlyCollection<IInventoryCell<ILoot>> Collection
        {
            get;
        }

        ILoot Peek();

        bool TryPop(out ILoot loot);

        bool TryPush(ILoot loot);
        void Clear();
    }
}