using Infrastructure.Services.Interfaces;
using Logic.Gameplay.Player;

namespace Infrastructure.Services.Player
{
    public interface IPersistentPlayerService : IService
    {
        public PlayerComponents Player { get; set; }
    }
}