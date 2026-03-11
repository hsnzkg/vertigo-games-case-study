using EventBus.Runtime;
using Project.Scripts.EventBus.Events.Wheel.Game;
using Project.Scripts.EventBus.Events.Wheel.UI;
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
            View.SpinPress += OnSpinPressed;
            View.SpinComplete += OnSpinCompleted;
            EventBus<EPrepareGame>.Register(m_prepareGameBind);
            EventBus<EGameStart>.Register(m_gameStartBind);
        }

        public override void Disable()
        {
            View.SpinPress -= OnSpinPressed;
            EventBus<EPrepareGame>.Unregister(m_prepareGameBind);
            EventBus<EGameStart>.Unregister(m_gameStartBind);
        }

        private void OnGamePrepare(EPrepareGame obj)
        {
            View.ChangeWheelZone(obj.ZoneType);
            for (int i = 0; i < obj.Result.Length; i++)
            {
                View.ChangeItem(i, obj.Result[i]);
            }
        }

        private void OnSpinPressed()
        {
            View.SetButtonInteractivity(false);
            EventBus<ESpinPressed>.Raise(new ESpinPressed());
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