using Logic.Gameplay.Player;

namespace Infrastructure.Services.Player
{
    public class PersistentPlayerService : IPersistentPlayerService
    {
        public PlayerComponents Player { get; set; }
    }
}