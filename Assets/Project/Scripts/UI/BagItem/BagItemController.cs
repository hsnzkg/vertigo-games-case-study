using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.BagItem
{
    public class BagItemController : ControllerBase<BagItemView, BagItemModel>
    {
        public BagItemController(BagItemView view, BagItemModel model) : base(view, model)
        {
        }

        public void Initialize(int id, UnityEngine.Sprite icon, int amount)
        {
            Model.Id = id;
            Model.Icon.Value = icon;
            Model.Amount.Value = amount;
        }

        public override void Enable()
        {
            Model.Amount.OnChanged += OnAmountChanged;
            Model.Icon.OnChanged += OnIconChanged;
            
            // Sync initial state
            OnAmountChanged(Model.Amount.Value);
            OnIconChanged(Model.Icon.Value);
        }

        public override void Disable()
        {
            Model.Amount.OnChanged -= OnAmountChanged;
            Model.Icon.OnChanged -= OnIconChanged;
        }

        private void OnAmountChanged(int amount)
        {
            View.SetAmount(amount);
        }

        private void OnIconChanged(UnityEngine.Sprite icon)
        {
            View.SetIcon(icon);
        }
    }
}
