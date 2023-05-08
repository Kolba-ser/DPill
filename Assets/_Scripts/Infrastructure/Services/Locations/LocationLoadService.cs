using Assets._Scripts.Logic.Level.Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Player;
using Infrastructure.Services.Providers.Assets;
using Logic.Level;
using StaticData.Locations;

namespace Infrastructure.Services.Locations
{
    public class LocationLoadService : ILocationLoadService
    {
        private readonly IGameFactory gameFactory;
        private readonly IAssetsProvider assetsProvider;
        private readonly LocationsInfoContainer locations;
        
        public LocationMetaData ActiveLocation { get; private set; }

        public LocationLoadService(IGameFactory gameFactory, IAssetsProvider assetsProvider)
        {
            this.gameFactory = gameFactory;
            this.assetsProvider = assetsProvider;
            this.locations = assetsProvider.Load<LocationsInfoContainer>(AssetsPath.LOCATIONS_PATH);
        }


        public void LoadLocation(int index)
        {
            var factory = new AssetFactory<LocationMetaData>(locations.GetByIndex(index), assetsProvider, false, false);
            ActiveLocation = factory.CreateAsset();
            ActiveLocation.Spawner.Initialize(gameFactory);
            ActiveLocation.Spawner.StartSpawn();

        }
    }
}