using Infrastructure;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Logic.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup screen;
        [SerializeField] private LoadProgressBar progressBar;

        private SceneLoader loader;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void SetSceneLoader(SceneLoader sceneLoader)
        {
            loader = sceneLoader;
            loader.ObserveEveryValueChanged(x => x.loadProgressValue)
                .TakeWhile(x => x < 0.99f)
                .Finally(() => progressBar.UpdateProgress(1f))
                .Subscribe(x => progressBar.UpdateProgress(x));
        }

        public void Show()
        {
            gameObject.SetActive(true);
            screen.alpha = 1;
        }

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(0.2f);

            while (screen.alpha > 0)
            {
                screen.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            Destroy(gameObject);
        }
    }
}
