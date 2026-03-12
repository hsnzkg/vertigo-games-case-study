using System.Collections.Generic;
using EventBus.Runtime;
using Project.Scripts.EventBus.Events.Wheel.Game;
using Project.Scripts.EventBus.Events.Wheel.UI;
using Project.Scripts.Game.WheelGame.Data.Provider;
using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.Wheel
{
    public class WheelController : ControllerBase<WheelView, WheelModel>
    {
        private readonly EventBind<EPrepareGame> m_prepareGameBind;
        private readonly EventBind<EGameStart> m_gameStartBind;
        public WheelController(WheelView view, WheelModel model) : base(view, model)
        {
            m_prepareGameBind = new EventBind<EPrepareGame>(OnGamePrepare);
            m_gameStartBind = new EventBind<EGameStart>(OnGameStart);
        }

        public override void Enable()
        {
            Model.CurrentZoneType.OnChanged += OnCurrentZoneChanged;
            Model.CurrentWheelItems.OnChanged += OnWheelItemsChanged;
            
            View.SpinPress += OnSpinPressed;
            View.WithdrawPress += OnWithDrawPressed;
            View.SpinComplete += OnSpinCompleted;
            EventBus<EPrepareGame>.Register(m_prepareGameBind);
            EventBus<EGameStart>.Register(m_gameStartBind);
        }

        public override void Disable()
        {
            Model.CurrentZoneType.OnChanged -= OnCurrentZoneChanged;
            Model.CurrentWheelItems.OnChanged -= OnWheelItemsChanged;
            
            View.SpinPress -= OnSpinPressed;
            View.WithdrawPress -= OnWithDrawPressed;
            View.SpinComplete -= OnSpinCompleted;
            EventBus<EPrepareGame>.Unregister(m_prepareGameBind);
            EventBus<EGameStart>.Unregister(m_gameStartBind);
        }

        private void OnWheelItemsChanged(IReadOnlyList<WheelItemResult> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                View.ChangeItem(i, result[i]);
            }
        }

        private void OnGamePrepare(EPrepareGame obj)
        {
            Model.CurrentZoneType.Set(obj.ZoneType);
            for (int i = 0; i < obj.Result.Length; i++)
            {
                Model.CurrentWheelItems.ReplaceAll(obj.Result);
            }
        }

        private void OnCurrentZoneChanged(WheelZoneType zoneType)
        {
            View.SetWithDrawButtonInteractivity(zoneType is WheelZoneType.SAFE or WheelZoneType.SUPER);
            View.ChangeWheelZone(zoneType);
        }

        private void OnSpinPressed()
        {
            View.SetWithDrawButtonInteractivity(false);
            View.SetButtonInteractivity(false);
            EventBus<ESpinPressed>.Raise(new ESpinPressed());
        }

        private void OnWithDrawPressed()
        {
        }

        private void OnGameStart(EGameStart obj)
        {
            View.Rotate(obj.Index);
        }

        private void OnSpinCompleted()
        {
            View.SetButtonInteractivity(true);
            EventBus<ESpinCompleted>.Raise(new ESpinCompleted());
        }
    }
}