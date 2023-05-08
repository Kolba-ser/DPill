using Infrastructure.Entities.Progress;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.SaveLoad
{
    public interface ISaveLoadProgressService : IService
    {
        public void Save();

        public PlayerProgress Load();
    }
}