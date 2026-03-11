using Project.Scripts.Game.WheelGame.Data.Provider;
using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.WheelItem
{
    public class WheelItemController : ControllerBase<WheelItemView,WheelItemModel>
    {
        public WheelItemController(WheelItemView view, WheelItemModel model) : base(view, model)
        {
        }

        public void ChangeItem(WheelItemResult wheelItemResult)
        {
            View.ChangeItem(wheelItemResult.Item.Sprite,wheelItemResult.Amount);
        }
    }
}