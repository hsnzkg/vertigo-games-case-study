using EventBus.Runtime;
using Project.Scripts.EventBus.Events.GamePlay;
using Project.Scripts.Managers;
using Project.Scripts.UI.Core;
using UnityEngine;

namespace Project.Scripts.UI.GamePlay
{
    public class GamePlayController : ControllerBase<GamePlayView, GamePlayModel>
    {
        private readonly EventBind<EMoneyProgress> m_moneyProgressBind;

        public GamePlayController(GamePlayView view, GamePlayModel model) : base(view, model)
        {
            m_moneyProgressBind = new EventBind<EMoneyProgress>(OnMoneyProgress);
            View.SetMoneyText(CurrencyManager.GetMoney());
        }

        public override void Enable()
        {
            Model.CurrentMoney.OnChanged += OnMoneyChanged;
            EventBus<EMoneyProgress>.Register(m_moneyProgressBind);
        }

        public override void Disable()
        {
            Model.CurrentMoney.OnChanged -= OnMoneyChanged;
            EventBus<EMoneyProgress>.Unregister(m_moneyProgressBind);
        }

        private void OnMoneyProgress(EMoneyProgress obj)
        {
            Model.CurrentMoney.Value = obj.CurrentMoneyValue;
        }

        private void OnMoneyChanged(int amount)
        {
            View.SetMoneyText(amount);
        }
    }
}
