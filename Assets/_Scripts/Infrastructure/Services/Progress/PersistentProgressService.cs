using Infrastructure.Entities.Progress;

namespace Infrastructure.Services.Progress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private PlayerProgress progress;

        private bool isProgressSet;

        public PlayerProgress Progress => progress;

        public void SetProgress(PlayerProgress playerProgress)
        {
            if (!isProgressSet)
            {
                progress = playerProgress;
                isProgressSet = true;
            }
        }
    }
}