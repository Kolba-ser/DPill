using Logic.Gameplay.Damage;

namespace Logic.Gameplay.Player
{
    public class PlayerDeathTraker
    {
        private EntityHealth playerHealth;

        public PlayerDeathTraker(EntityHealth playerHealth)
        {
            this.playerHealth = playerHealth;
            playerHealth.OnDeath += ShowDeathScreen;
        }

        private void ShowDeathScreen(EntityHealth _)
        {
            playerHealth.OnDeath -= ShowDeathScreen;
            DeathScreen.Instance.Show();
        }
    }
}