using Infrastructure.Entities.Progress;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Progress
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress Progress { get; }

        public void SetProgress(PlayerProgress playerProgress);
    }
}