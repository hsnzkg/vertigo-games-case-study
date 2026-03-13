using Project.Scripts.UI.Core;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.GamePlay
{
    public class GamePlayView : ViewBase
    {
        [SerializeField]
        private TextMeshProUGUI m_moneyText;

        protected override void Initialize()
        {
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }

        protected override void OnDestroy()
        {
        }

        public void SetMoneyText(int amount)
        {
            if (m_moneyText != null)
            {
                m_moneyText.text = $"<sprite name=\"UI_icon_gold\">{amount}";
            }
        }
    }
}
