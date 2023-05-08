using Infrastructure.Initializers;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Guns;
using Infrastructure.Services.Input;
using Infrastructure.Services.Invenentory.Model.StackModel;
using Infrastructure.Services.Locations;
using Logic.Gameplay.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace Infrastructure.Initializers
{
    public class PlayerInitializer : GameEntityInitializer<PlayerComponents>
    {
        public PlayerInitializer(AllServices allServices) : base(allServices)
        {
        }

        public override PlayerComponents CreateAndInitialize()
        {
            var gameFactory = allServices.GetSingle<IGameFactory>();

            PlayerComponents playerComponents = gameFactory.CreateAsset<PlayerComponents>("Player");
            Transform spawnPoint = allServices.GetSingle<ILocationLoadService>().ActiveLocation.PlayerSpawnPoint;
            playerComponents.Movement.Initialize(spawnPoint);
            return playerComponents;
        }

        public override void Initialize(PlayerComponents entity)
        {
            Transform spawnPoint = allServices.GetSingle<ILocationLoadService>().ActiveLocation.PlayerSpawnPoint;
            var weaponSelectionService = allServices.GetSingle<IWeaponSelectionService>();
            var lootInventory = allServices.GetSingle<ILootInventoryService>();

            new PlayerDeathTraker(entity.Health);
            entity.PlayerAttack.SetWeapon(weaponSelectionService.SpawnActiveGun());
            entity.Movement.Initialize(spawnPoint);
            entity.Inventory.Initialize(lootInventory);
            entity.LootCollector.Initialize(lootInventory);
        }
    }
}
