using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Restart
{
    public interface IGameRestartService : IService
    {
        void Restart();
    }
}