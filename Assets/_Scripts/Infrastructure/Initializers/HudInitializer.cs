using Infrastructure.Services;
using Infrastructure.Services.Currency;
using Infrastructure.Services.Factory;
using Logic.UI.HUD;

namespace Infrastructure.Initializers
{
    public class HudInitializer : GameEntityInitializer<HudComponents>
    {
        public HudInitializer(AllServices allServices) : base(allServices)
        {
        }

        public override HudComponents CreateAndInitialize()
        {
            var gameFactory = allServices.GetSingle<IGameFactory>();
            HudComponents hudComponents = gameFactory.CreateAsset<HudComponents>("HUD");
            Initialize(hudComponents);
            return hudComponents;
        }

        public override void Initialize(HudComponents entity)
        {
            entity.GemsPresenter.Initialize(allServices.GetSingle<IGemsService>());
            entity.MoneyPresenter.Initialize(allServices.GetSingle<IMoneyService>());
        }
    }
}