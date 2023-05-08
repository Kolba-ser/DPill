using Assets._Scripts.Logic.Level.Data;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Providers.Assets;
using Logic;
using Logic.Gameplay.Enemy;
using Logic.Gameplay.Player;
using Logic.UI.HUD;

namespace Infrastructure.Initializers
{
    internal class GameFactoryInitializer : GameEntityInitializer<IGameFactory>
    {
        public GameFactoryInitializer(AllServices allServices) : base(allServices)
        {
        }

        public override IGameFactory CreateAndInitialize()
        {
            GameFactory gameFactory = new GameFactory();
            Initialize(gameFactory);

            return gameFactory;
        }

        public override void Initialize(IGameFactory entity)
        {
            RegisterSubFactories(entity);
        }

        private void RegisterSubFactories(IGameFactory gameFactory)
        {
            var assetsProvider = allServices.GetSingle<IAssetsProvider>();

            gameFactory.RegisterSubFactory(new AssetFactory<HudComponents>(AssetsPath.HUD_PATH, assetsProvider));
            gameFactory.RegisterSubFactory(new AssetFactory<EnemyComponents>(AssetsPath.ENEMY_PATH, assetsProvider));
            gameFactory.RegisterSubFactory(new AssetFactory<Loot>(AssetsPath.MONEY_LOOT_PATH, assetsProvider));
            gameFactory.RegisterSubFactory(new AssetFactory<Loot>(AssetsPath.GEMS_LOOT_PATH, assetsProvider));
            gameFactory.RegisterSubFactory(new InitializableAssetFactory<PlayerComponents>(AssetsPath.PLAYER_PATH, assetsProvider, new PlayerInitializer(allServices).Initialize));
        }
    }
}