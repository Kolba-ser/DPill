using Assets._Scripts.Logic.Level.Data;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Locations
{
    public interface ILocationLoadService : IService
    {
        public LocationMetaData ActiveLocation
        {
            get;
        }

        public void LoadLocation(int index);
    }
}