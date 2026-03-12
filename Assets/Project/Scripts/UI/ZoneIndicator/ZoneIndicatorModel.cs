using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.ZoneIndicator
{
    public class ZoneIndicatorModel : IModel
    {
        public readonly Observable<int> CurrentZoneIndex = new(-1);
    }
}
