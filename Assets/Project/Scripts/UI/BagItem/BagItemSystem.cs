using Project.Scripts.UI.Core;
using UnityEngine;

namespace Project.Scripts.UI.BagItem
{
    public class BagItemSystem : SystemBase<BagItemModel, BagItemView, BagItemController>
    {
        protected override BagItemModel CreateModel()
        {
            return new BagItemModel();
        }

        protected override BagItemController CreateController(BagItemView view, BagItemModel model)
        {
            return new BagItemController(view, model);
        }
    }
}
