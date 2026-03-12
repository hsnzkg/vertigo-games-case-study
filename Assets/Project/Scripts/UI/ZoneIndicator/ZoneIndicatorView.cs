using Project.Scripts.UI.Core;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.ZoneIndicator
{
    public class ZoneIndicatorView : ViewBase
    {
        [SerializeField]
        private TextMeshProUGUI m_currentZoneIndexText;

        [SerializeField]
        private TextMeshProUGUI m_safeZoneIndexText;

        [SerializeField]
        private TextMeshProUGUI m_superZoneIndexText;

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

        public void SetCurrentZoneText(int index)
        {
            m_currentZoneIndexText.text = $"{index}";
        }

        public void SetSafeZoneText(int index)
        {
            m_safeZoneIndexText.text = $"{index}";
        }

        public void SetSuperZoneText(int index)
        {
            m_superZoneIndexText.text = $"{index}";
        }
    }
}
