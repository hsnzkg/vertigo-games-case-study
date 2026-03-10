using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.Wheel
{
    public class WheelController : ControllerBase<WheelView, WheelModel>
    {
        public WheelController(WheelView view, WheelModel model) : base(view, model)
        {
        }

        public override void Initialize()
        {
            View.FitItemTargets();
        }
    }
}