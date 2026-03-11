using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.WheelItem
{
    public class WheelItemSystem : SystemBase<WheelItemModel,WheelItemView,WheelItemController>
    {
        protected override WheelItemModel CreateModel()
        {
            return new WheelItemModel();
        }

        protected override WheelItemController CreateController(WheelItemView view, WheelItemModel model)
        {
            return new WheelItemController(view,model);
        }
    }
}