using TMPro;
using UnityEngine;

namespace Logic.UI.HUD.Currency.Money
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        public void UpdateView(int value) =>
            valueText.text = value.ToString();
    }
}