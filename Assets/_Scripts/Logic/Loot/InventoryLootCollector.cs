using DG.Tweening;
using Infrastructure.Services;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Invenentory.Model.StackModel;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class InventoryLootCollector : MonoBehaviour
    {
        private IMoneyService moneyService;
        private IGemsService gemsService;
        private ILootInventoryService lootInventory;

        private void Start()
        {
            moneyService = AllServices.Container.GetSingle<IMoneyService>();
            gemsService = AllServices.Container.GetSingle<IGemsService>();
            lootInventory = AllServices.Container.GetSingle<ILootInventoryService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            TakeLootFromInventory();
        }

        private void TakeLootFromInventory()
        {
            int count = lootInventory.Collection.Count;

            for (int i = 0; i < count; i++)
            {
                if (lootInventory.TryPop(out ILoot loot))
                {
                    Vector3 direction = Random.insideUnitSphere.normalized;
                    
                    loot.transform.DOKill();
                    loot.transform.DOMove(loot.transform.position + direction, 1f)
                        .OnComplete(() => 
                        {
                            Collect(loot);
                            Destroy(loot.transform.gameObject);
                        });
                }
            }

           
        }

        private void OnTriggerExit(Collider other)
        {

        }

        private void Collect(ILoot loot)
        {
            switch (loot.LootType)
            {
                case Currency.Money:
                    moneyService.Add(loot.Value);
                    break;
                case Currency.Gems:
                    gemsService.Add(loot.Value);
                    break;

                default:
                    break;
            }
        }
    }
}