using Infrastructure.Services.Currency;
using UnityEngine;

namespace Logic.UI.HUD.Currency.Gems
{
    public class GemsPresenter : MonoBehaviour
    {
        [SerializeField] private GemsView view;
        private IGemsService gemsService;

        private void OnDestroy() => 
            gemsService.OnValueChanged -= UpdateView;

        public void Initialize(IGemsService gemsService)
        {
            this.gemsService = gemsService;
            gemsService.OnValueChanged += UpdateView;
        }

        private void UpdateView(int value) =>
            view.UpdateView(value);
    }
}