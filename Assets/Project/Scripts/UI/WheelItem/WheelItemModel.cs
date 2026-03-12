using Project.Scripts.Game.WheelGame.Data.Item;
using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.WheelItem
{
    public class WheelItemModel : IModel
    {
        public Observable<IWheelItem> Item = new();
        public Observable<int> Amount = new();
    }
}