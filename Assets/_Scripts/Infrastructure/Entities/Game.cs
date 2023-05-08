using Infrastructure.Bootstrap;
using Infrastructure.Services;
using Infrastructure.State;
using Logic.Loading;
using System;
using UnityEngine;

namespace Infrastructure
{
    public class Game : IDisposable
    {
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);
            loadingScreen.SetSceneLoader(sceneLoader);
            _stateMachine = new GameStateMachine(sceneLoader, loadingScreen, AllServices.Container);
        }

        public void Dispose()
        { 
            AllServices.Container.Dispose();
            _stateMachine.Dispose();
            _stateMachine = null;
        }
    }
}