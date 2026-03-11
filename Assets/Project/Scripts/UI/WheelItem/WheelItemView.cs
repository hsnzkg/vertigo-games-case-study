using Project.Scripts.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.WheelItem
{
    public class WheelItemView : ViewBase
    {
        [SerializeField] private Image m_itemImage;
        [SerializeField] private TextMeshProUGUI m_itemAmountText;

        public void ChangeItem(Sprite image, int amount)
        {
            m_itemImage.sprite = image;
            m_itemAmountText.text = GetAmountText(amount);
        }

        private static string GetAmountText(int amount)
        {
            return $"x{amount}";
        }
    }
}