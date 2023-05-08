using Infrastructure.Bootstrap;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        public float loadProgressValue { get; private set; }

        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
            while (!waitNextScene.isDone)
            {
                loadProgressValue = waitNextScene.progress / 0.9f;
                yield return null;
            }

            loadProgressValue = 1;

            onLoaded?.Invoke();
        }
    }
}
