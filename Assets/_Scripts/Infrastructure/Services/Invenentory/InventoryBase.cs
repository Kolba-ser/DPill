using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Invenentory
{
    public abstract class InventoryBase<T>
    {
        public abstract IReadOnlyCollection<IInventoryCell<T>> Collection
        {
            get;
        }
    }
}