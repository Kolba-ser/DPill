using Infrastructure.Services;
using Infrastructure.Services.Player;
using Infrastructure.Services.Restart;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : Singleton<DeathScreen>
{
    [SerializeField] private Button restartButton;

    protected override void AwakeSingletone()
    {
        base.AwakeSingletone();
        restartButton.onClick.AddListener(Restart);
        Hide();
    }

    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);

    public void DebugDeath()
    {
        AllServices.Container.GetSingle<IPersistentPlayerService>().Player.Health.Decrease(int.MaxValue);
    }

    private void Restart() =>
        AllServices.Container.GetSingle<IGameRestartService>().Restart();
}