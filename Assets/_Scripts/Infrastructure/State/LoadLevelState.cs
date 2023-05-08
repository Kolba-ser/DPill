using Infrastructure.Initializers;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.Input.Mobile;
using Infrastructure.Services.Locations;
using Infrastructure.Services.Player;
using Infrastructure.Services.Progress;
using Logic.Gameplay.Player;
using Logic.Loading;
using Logic.UI.HUD;
using System;
using UnityEngine;

namespace Infrastructure.State
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingScreen loadingScreen;
        private readonly IGameFactory gameFactory;

        private AllServices allServices;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory, AllServices allServices)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
            this.gameFactory = gameFactory;
            this.allServices = allServices;
        }

        public void Enter(string sceneName)
        {
            sceneLoader.Load(sceneName, OnLoaded);
            loadingScreen.Show();
        }

        private void InitializeGameWorld()
        {
            CreateHUD();
            CreateLocation();
            CreatePlayer();
            InitializeMainCamera();
        }

        private void CreateHUD()
        {
            HudInitializer hudInitializer = new HudInitializer(allServices);
            HudComponents hudComponents = hudInitializer.CreateAndInitialize();
            MobileInputService inputService = new MobileInputService(hudComponents.Joystick, Camera.main);
            allServices.RegisterSingle<IInputService>(inputService);
        }

        private void CreateLocation()
        {
            int index = allServices.GetSingle<IPersistentProgressService>().Progress.LocationIndex;
            allServices.GetSingle<ILocationLoadService>().LoadLocation(index);
        }

        private void InitializeMainCamera()
        {
            var initializer = new MainCameraInitilizer(allServices);
            initializer.CreateAndInitialize();
        }

        private void CreatePlayer()
        {
            PlayerComponents playerComponents = gameFactory.CreateAsset<PlayerComponents>("Player");
            var playerService = new PersistentPlayerService();
            playerService.Player = playerComponents;
            allServices.RegisterSingle<IPersistentPlayerService>(playerService);
        }

        public void Exit()
        {
            loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            InitializeGameWorld();

            gameStateMachine.Enter<GameLoopState>();
        }
    }
}