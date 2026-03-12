using EventBus.Runtime;
using Project.Scripts.EventBus.Events.Wheel.Game;
using Project.Scripts.UI.Core;
using UnityEngine;

namespace Project.Scripts.UI.ZoneIndicator
{
    public class ZoneIndicatorController : ControllerBase<ZoneIndicatorView, ZoneIndicatorModel>
    {
        private readonly EventBind<EPrepareGame> m_prepareGameBind;

        public ZoneIndicatorController(ZoneIndicatorView view, ZoneIndicatorModel model) : base(view, model)
        {
            m_prepareGameBind = new EventBind<EPrepareGame>(OnGamePrepare);
        }

        public override void Enable()
        {
            Debug.Log("Enable Zone Indicator Controller");
            Model.CurrentZoneIndex.OnChanged += OnZoneIndexChanged;
            EventBus<EPrepareGame>.Register(m_prepareGameBind);
        }

        public override void Disable()
        {
            Debug.Log("Disable Zone Indicator Controller");
            Model.CurrentZoneIndex.OnChanged -= OnZoneIndexChanged;
            EventBus<EPrepareGame>.Unregister(m_prepareGameBind);
        }

        private void OnZoneIndexChanged(int index)
        {
            int currentZone = index + 1;

            View.SetCurrentZoneText(currentZone);

            int nextSafeZone = ((currentZone / 5) + 1) * 5;
            int nextSuperZone = ((currentZone / 30) + 1) * 30;

            View.SetSafeZoneText(nextSafeZone);
            View.SetSuperZoneText(nextSuperZone);
        }

        private void OnGamePrepare(EPrepareGame obj)
        {
            Model.CurrentZoneIndex.Value = obj.ZoneIndex;
        }
    }
}
