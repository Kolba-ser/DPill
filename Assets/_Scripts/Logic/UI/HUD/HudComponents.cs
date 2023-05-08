using Logic.UI.HUD.Currency.Gems;
using Logic.UI.HUD.Currency.Money;
using UnityEngine;

namespace Logic.UI.HUD
{
    public class HudComponents : MonoBehaviour
    {
        [field: SerializeField] 
        public Joystick Joystick{ get; private set; }

        [field: SerializeField]
        public MoneyPresenter MoneyPresenter { get; private set; }

        [field: SerializeField]
        public GemsPresenter GemsPresenter { get; private set; }

    }
}