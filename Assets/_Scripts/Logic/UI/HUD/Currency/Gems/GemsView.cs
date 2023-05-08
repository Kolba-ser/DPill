using TMPro;
using UnityEngine;

namespace Logic.UI.HUD.Currency.Gems
{
    public class GemsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        public void UpdateView(int value) => 
            valueText.text = value.ToString();
    }
}