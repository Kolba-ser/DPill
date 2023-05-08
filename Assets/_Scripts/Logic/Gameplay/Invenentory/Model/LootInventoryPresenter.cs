using DG.Tweening;
using Infrastructure.Services.Invenentory;
using Infrastructure.Services.Invenentory.Model.StackModel;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Gameplay.Invenentory.Model
{
    public class LootInventoryPresenter : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Vector2 cellsOffset;
        [SerializeField, Range(1, 3)] private int columns;
#if UNITY_EDITOR

        [Header("Debug")]
        [SerializeField] private Vector3 cellSize;

        [SerializeField] private int debugCellsCount;

#endif

        private List<InventoryCellPresener> cells;
        private ILootInventoryService lootInventory;

        private void OnDestroy() =>
            UnsubscribeFromInteractions();

        public void Initialize(ILootInventoryService lootInventoryService)
        {
            this.cells = new List<InventoryCellPresener>(lootInventoryService.CellsCount);
            this.lootInventory = lootInventoryService;

            FillCells();

            SubscribeOnInteractions();
        }

        private void FillCells()
        {
            int cellsCount = lootInventory.CellsCount;
            int currentColumn = 0;
            int cellsInColumn = cellsCount / columns;
            int cellInColumn = 0;

            for (int i = 0; i < cellsCount; i++)
            {
                if (i % cellsInColumn == 0 && i != 0)
                {
                    currentColumn++;
                    cellInColumn = 0;
                }

                Vector3 cellPosition = new Vector3
                    (
                    container.position.x,
                    container.position.y + cellsOffset.y * cellInColumn,
                    container.position.z + cellsOffset.x * currentColumn
                    );

                string parentName = i.ToString();
                GameObject cellParent = new GameObject(parentName);
                cellParent.transform.SetParent(container);
                cellParent.transform.position = cellPosition;

                InventoryCellPresener cell = new InventoryCellPresener(cellParent.transform);
                cells.Add(cell);
                cellInColumn++;
            }
        }

        private void SubscribeOnInteractions()
        {
            lootInventory.OnItemRemoved += OnItemRemoved;
            lootInventory.OnItemAdded += OnItemAdded;
        }

        private void UnsubscribeFromInteractions()
        {
            lootInventory.OnItemRemoved -= OnItemRemoved;
            lootInventory.OnItemAdded -= OnItemAdded;
        }

        private void OnItemRemoved() =>
            DestroyTopItem();

        private void OnItemAdded()
        {
            PutOnTop();
        }

        private void PutOnTop()
        {
            var cell = FirstEmptyCell();
            cell.Put(lootInventory.Peek());
        }

        private void DestroyTopItem()
        {
            var cell = LastFullCell();
            cell.Take();
        }

        private InventoryCellPresener FirstEmptyCell()
        {
            foreach (var cell in cells)
            {
                if (cell.IsEmpty)
                    return cell;
            }

            return default(InventoryCellPresener);
        }

        private InventoryCellPresener LastFullCell()
        {
            for (int i = cells.Count - 1; i >= 0; i--)
            {
                InventoryCellPresener cell = cells[i];
                if (!cell.IsEmpty)
                    return cell;
            }

            return default(InventoryCellPresener);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            int currentColumn = 0;
            int cellsInColumn = debugCellsCount / columns;
            int cellInColumn = 0;

            for (int i = 0; i < debugCellsCount; i++)
            {
                if (i % cellsInColumn == 0 && i != 0)
                {
                    currentColumn++;
                    cellInColumn = 0;
                }

                Vector3 cellPosition = new Vector3
                    (
                    container.position.x,
                    container.position.y + cellsOffset.y * cellInColumn,
                    container.position.z + cellsOffset.x * currentColumn
                    );

                Gizmos.DrawWireCube(cellPosition, cellSize);
                cellInColumn++;
            }
        }

#endif

        private class InventoryCellPresener : IInventoryCell<ILoot>
        {
            public Transform Parent;

            public InventoryCellPresener(Transform parent)
            {
                Parent = parent;
                IsEmpty = true;
            }

            public bool IsEmpty
            {
                get; private set;
            }

            public ILoot StoredEntity
            {
                get; private set;
            }

            public void Put(ILoot entity)
            {
                if (!IsEmpty || entity == null)
                    return;

                entity.transform.SetParent(Parent);

                Vector3[] path = new Vector3[]
                {
                    new Vector3(Parent.right.x * 1.5f, Parent.right.y, Parent.right.z),
                    Parent.localPosition
                };
                entity.transform.DOKill();
                entity.transform.DOLocalPath(path, 0.7f);
                


                IsEmpty = false;
                StoredEntity = entity;
            }

            public ILoot Take()
            {
                var tempEntity = StoredEntity;
                StoredEntity = default(ILoot);
                IsEmpty = true;
                tempEntity?.transform.SetParent(null);
                return tempEntity;
            }
        }
    }
}