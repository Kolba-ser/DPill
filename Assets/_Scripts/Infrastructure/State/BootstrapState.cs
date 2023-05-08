using Infrastructure.Initializers;
using Infrastructure.SaveLoad;
using Infrastructure.Services;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Guns;
using Infrastructure.Services.Invenentory.Model.StackModel;
using Infrastructure.Services.Locations;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Providers.Assets;
using Infrastructure.Services.Restart;
using Logic.Gameplay.Player;
using Logic.UI.HUD;
using UnityEngine;

namespace Infrastructure.State
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly AllServices allServices;
        private AssetsProvider assetsProvider;
        private IGameFactory gameFactory;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.allServices = allServices;

            RegisterServices();
        }

        public void Enter() =>
            sceneLoader.Load(GameScenes.INITIAL_SCENE, EnterLoadLevel);

        public void Exit()
        {
        }

        private async void EnterLoadLevel()
        {
            await gameFactory.Payload();
            gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            assetsProvider = new AssetsProvider();
            allServices.RegisterSingle<IAssetsProvider>(assetsProvider);

            RegisterGameFactory();

            PersistentProgressService progressService = new PersistentProgressService();
            allServices.RegisterSingle<IPersistentProgressService>(progressService);

            allServices.RegisterSingle<ISaveLoadProgressService>(new SaveLoadProgressService());
            allServices.RegisterSingle<ILocationLoadService>(new LocationLoadService(gameFactory, assetsProvider));
            allServices.RegisterSingle<IWeaponSelectionService>(new WeaponSelectionService(assetsProvider, allServices));
            allServices.RegisterSingle<IGameRestartService>(new GameRestartService(gameStateMachine));
            allServices.RegisterSingle<ILootInventoryService>(new LootInventoryService());

            allServices.RegisterSingle<IMoneyService>(new MoneyService(progressService));
            allServices.RegisterSingle<IGemsService>(new GemsService(progressService));
        }

        private void RegisterGameFactory()
        {
            var initializer = new GameFactoryInitializer(allServices);
            gameFactory = initializer.CreateAndInitialize();
            allServices.RegisterSingle<IGameFactory>(gameFactory);
        }
    }
}