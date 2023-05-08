using Infrastructure.Services.Currency;
using Logic.UI.HUD.Currency.Gems;
using UnityEngine;

namespace Logic.UI.HUD.Currency.Money
{
    public class MoneyPresenter : MonoBehaviour
    {
        [SerializeField] private MoneyView view;
        private IMoneyService MoneyService;

        private void OnDestroy() =>
            MoneyService.OnValueChanged -= UpdateView;

        public void Initialize(IMoneyService moneyService)
        {
            this.MoneyService = moneyService;
            moneyService.OnValueChanged += UpdateView;
        }

        private void UpdateView(int value) =>
            view.UpdateView(value);
    }
}