using Project.Scripts.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.BagItem
{
    public class BagItemView : ViewBase
    {
        [SerializeField]
        private Image m_iconImage;

        [SerializeField]
        private TextMeshProUGUI m_amountText;

        public void SetIcon(Sprite icon)
        {
            m_iconImage.sprite = icon;
        }

        public void SetAmount(int amount)
        {
            m_amountText.text = amount.ToString();
        }
    }
}
