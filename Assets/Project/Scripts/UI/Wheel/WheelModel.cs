using Project.Scripts.Game.WheelGame.Data.Provider;
using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.Wheel
{
    public class WheelModel : IModel
    {
        public readonly Observable<WheelZoneType> CurrentZoneType = new();
        public readonly ObservableList<WheelItemResult> CurrentWheelItems = new();
    }
}