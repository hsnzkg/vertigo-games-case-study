using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.Wheel
{
    public class WheelSystem : SystemBase<WheelView, WheelModel, WheelController>
    {
        protected override WheelModel CreateModel()
        {
            return new WheelModel();
        }

        protected override WheelController CreateController(WheelView view, WheelModel model)
        {
            return new WheelController(view, model);
        }
    }
}