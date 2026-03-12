using Project.Scripts.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.RevivePanel
{
    public class RevivePanelView : ViewBase
    {
        [SerializeField]
        private Button m_reviveButton;

        [SerializeField]
        private Button m_giveUpButton;

        public event System.Action RevivePress;
        public event System.Action GiveUpPress;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_reviveButton.onClick.AddListener(OnRevivePressed);
            m_giveUpButton.onClick.AddListener(OnGiveUpPressed);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_reviveButton.onClick.RemoveListener(OnRevivePressed);
            m_giveUpButton.onClick.RemoveListener(OnGiveUpPressed);
        }

        public override void Open()
        {
            Root.gameObject.SetActive(true);
            SetInteractivity(true);
        }

        public void SetInteractivity(bool value)
        {
            m_reviveButton.interactable = value;
            m_giveUpButton.interactable = value;
        }

        private void OnRevivePressed()
        {
            RevivePress?.Invoke();
        }

        private void OnGiveUpPressed()
        {
            GiveUpPress?.Invoke();
        }

        public override void Close()
        {
            Root.gameObject.SetActive(false);
        }
    }
}
