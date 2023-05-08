using Infrastructure.Entities.Progress;
using Infrastructure.SaveLoad;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Infrastructure.State
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly IPersistentProgressService progressService;
        private readonly ISaveLoadProgressService saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadProgressService saveLoadService)
        {
            this.stateMachine = gameStateMachine;
            this.progressService = progressService;
            this.saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            stateMachine.Enter<LoadLevelState, string>(progressService.Progress.LastScene);
        }

        public void Exit()
        {
        }

        private PlayerProgress LoadProgressOrInitNew()
        {
            PlayerProgress loaded = saveLoadService.Load();
            PlayerProgress progress = loaded ?? NewProgress();
            progressService.SetProgress(progress);

            return progress;
        }

        private PlayerProgress NewProgress()
        {
            Debug.Log("NewProgress");

            PlayerProgress newProgress = new PlayerProgress()
            {
                Gems = 0,
                Money = 0,
                LastScene = GameScenes.GAME_SCENE,
                LocationIndex = 0,
                GunName = "Pistol",
            };

            return newProgress;
        }
    }
}