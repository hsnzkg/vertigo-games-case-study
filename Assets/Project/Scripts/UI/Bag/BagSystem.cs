using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.Bag
{
    public class BagSystem : SystemBase<BagModel, BagView, BagController>
    {
        protected override BagModel CreateModel()
        {
            return new BagModel();
        }

        protected override BagController CreateController(BagView view, BagModel model)
        {
            return new BagController(view, model);
        }
    }
}
    