using EventBus.Runtime;
using Project.Scripts.EventBus.Events.Wheel.Game;
using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.RevivePanel
{
    public class RevivePanelController : ControllerBase<RevivePanelView, RevivePanelModel>
    {
        private readonly EventBind<EBombExplode> m_bombExplodeBind;

        public RevivePanelController(RevivePanelView view, RevivePanelModel model) : base(view, model)
        {
            m_bombExplodeBind = new EventBind<EBombExplode>(OnBombExplode);
        }

        public override void Initialize()
        {
            EventBus<EBombExplode>.Register(m_bombExplodeBind);
            View.CloseImmediately();
        }

        public override void Enable()
        {
            View.RevivePress += OnRevivePressed;
            View.GiveUpPress += OnGiveUpPressed;
        }

        public override void Disable()
        {
            View.RevivePress -= OnRevivePressed;
            View.GiveUpPress -= OnGiveUpPressed;
        }

        public override void Destroy()
        {
            EventBus<EBombExplode>.Unregister(m_bombExplodeBind);
        }

        private void OnRevivePressed()
        {
            View.SetInteractivity(false);
            View.Close();
        }

        private void OnGiveUpPressed()
        {
            View.SetInteractivity(false);
            EventBus<EGiveUp>.Raise(new EGiveUp());
            View.Close();
        }

        private void OnBombExplode()
        {
            View.Open();
        }
    }
}
