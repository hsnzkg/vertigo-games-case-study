using Project.Scripts.Game.WheelGame.Data.Item;
using Project.Scripts.Game.WheelGame.Data.Provider;
using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.WheelItem
{
    public class WheelItemController : ControllerBase<WheelItemView,WheelItemModel>
    {
        public WheelItemController(WheelItemView view, WheelItemModel model) : base(view, model)
        {
        }
        
        public override void Enable()
        {
            Model.Item.OnChanged += OnItemChanged;
            Model.Amount.OnChanged += OnAmountChanged;
        }

        public override void Disable()
        {
            Model.Item.OnChanged -= OnItemChanged;
            Model.Amount.OnChanged -= OnAmountChanged;
        }

        public void ChangeItem(WheelItemResult wheelItemResult)
        {
            Model.Item.Value = wheelItemResult.Item;
            Model.Amount.Value = wheelItemResult.Amount;
        }

        private void OnAmountChanged(int obj)
        {
            View.ChangeAmount(obj);
        }

        private void OnItemChanged(IWheelItem obj)
        {
            View.ChangeItemImage(obj.Sprite);
        }
    }
}