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

        public void ChangeItemImage(Sprite image)
        {
            m_itemImage.sprite = image;
        }

        public void ChangeAmount(int amount)
        {
            m_itemAmountText.text = GetAmountText(amount);
        }

        private static string GetAmountText(int amount)
        {
            return $"x{amount}";
        }
    }
}