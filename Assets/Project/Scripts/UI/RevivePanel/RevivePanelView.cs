using Project.Scripts.UI.Core;
using TMPro;
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

        [SerializeField]
        private int m_revivePrice;

        [SerializeField]
        private TextMeshProUGUI m_reviveText;

        public int RevivePrice => m_revivePrice;

        public event System.Action RevivePress;
        public event System.Action GiveUpPress;

        protected override void Awake()
        {
            m_reviveText.text = $"<sprite name=\"UI_icon_gold\">{m_revivePrice}\nREVIVE\n";
        }

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
            SetGiveUpButtonInteractivity(true);
        }

        public void SetGiveUpButtonInteractivity(bool value)
        {
            m_giveUpButton.interactable = value;
        }

        public void SetReviveButtonInteractivity(bool value)
        {
            m_reviveButton.interactable = value;
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
