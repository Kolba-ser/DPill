using Infrastructure.SaveLoad;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Progress;
using Logic.Loading;
using System;
using System.Collections.Generic;

namespace Infrastructure.State
{
    public class GameStateMachine : IDisposable
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;


        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, services.GetSingle<IGameFactory>(), services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.GetSingle<IPersistentProgressService>(), services.GetSingle<ISaveLoadProgressService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(RestartState)] = new RestartState(this, services.GetSingle<IGameFactory>(), services),
            };
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        public TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

        public void Dispose()
        {
            _states = null;
            _activeState = null;
        }
    }
}
