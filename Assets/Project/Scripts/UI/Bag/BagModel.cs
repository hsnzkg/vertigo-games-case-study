using System.Collections.Generic;
using Project.Scripts.UI.BagItem;
using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.Bag
{
    public class BagModel : IModel
    {
        public readonly ObservableList<BagItemSystem> Items = new();
    }
}
