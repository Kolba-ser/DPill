using Infrastructure.Initializers;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Invenentory.Model.StackModel;
using Infrastructure.Services.Player;
using Logic;
using Logic.Gameplay.Player;
using UnityEngine;

namespace Infrastructure.State
{
    public class RestartState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IGameFactory gameFactory;
        private readonly AllServices allServices;

        public RestartState(GameStateMachine gameStateMachine, IGameFactory gameFactory, AllServices allServices)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.allServices = allServices;
        }

        public void Enter()
        {
            RespawnPlayer();
            ReinitializeMainCamera();
            allServices.GetSingle<ILootInventoryService>().Clear();
            gameStateMachine.Enter<GameLoopState>();
            DeathScreen.Instance.Hide();
        }

        public void Exit()
        {
        }

        private void RespawnPlayer()
        {
            var persistentPlayerService = allServices.GetSingle<IPersistentPlayerService>();
            GameObject.Destroy(persistentPlayerService.Player.gameObject);
            PlayerComponents newPlayer = gameFactory.CreateAsset<PlayerComponents>("Player");
            persistentPlayerService.Player = newPlayer;
        }
        private void ReinitializeMainCamera()
        {
            var initializer = new MainCameraInitilizer(allServices);
            initializer.CreateAndInitialize();
        }
    }
}