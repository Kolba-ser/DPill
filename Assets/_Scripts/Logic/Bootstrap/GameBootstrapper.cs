using Infrastructure.State;
using Logic.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen loadingScreenPrefab;
        public static GameBootstrapper instance;

        private Game game;

        private void Awake()
        {
            if(instance != null && this != instance)
            {
                Destroy(gameObject);
                return;
            }

            if (instance != null && SceneManager.GetActiveScene().name != GameScenes.INITIAL_SCENE)
            {
                SceneManager.LoadScene(GameScenes.INITIAL_SCENE);
                return;
            }

            var loadscreen = Instantiate(loadingScreenPrefab);
            Init(loadscreen);
        }

        private void Init(LoadingScreen loadscreen)
        {
            game = new Game(this, loadscreen);
            game._stateMachine.Enter<BootstrapState>();
            instance = this;

            DontDestroyOnLoad(this);
        }
    }
}