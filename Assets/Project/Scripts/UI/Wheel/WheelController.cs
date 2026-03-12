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
        private readonly EventBind<EBombExplode> m_bombExplode;
        public WheelController(WheelView view, WheelModel model) : base(view, model)
        {
            m_prepareGameBind = new EventBind<EPrepareGame>(OnGamePrepare);
            m_gameStartBind = new EventBind<EGameStart>(OnGameStart);
            m_bombExplode = new EventBind<EBombExplode>(OnBombExplode);
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
            EventBus<EBombExplode>.Register(m_bombExplode);
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
            EventBus<EBombExplode>.Unregister(m_bombExplode);
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
            View.SetSpinButtonInteractivity(true);
            Model.CurrentZoneType.Value = obj.ZoneType;
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
            View.SetSpinButtonInteractivity(false);
            EventBus<ESpinPressed>.Raise(new ESpinPressed());
        }

        private void OnWithDrawPressed()
        {
            Model.CurrentZoneType.Value = WheelZoneType.DEFAULT;
        }

        private void OnGameStart(EGameStart obj)
        {
            View.Rotate(obj.Index);
        }

        private void OnSpinCompleted()
        {
            View.SetSpinButtonInteractivity(true);
            EventBus<ESpinCompleted>.Raise(new ESpinCompleted());
        }

        private void OnBombExplode()
        {
            View.SetSpinButtonInteractivity(false);
            View.SetWithDrawButtonInteractivity(false);
        }
    }
}