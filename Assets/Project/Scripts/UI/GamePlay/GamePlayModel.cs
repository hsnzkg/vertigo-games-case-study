using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.GamePlay
{
    public class GamePlayModel : IModel
    {
        public readonly Observable<int> CurrentMoney = new(0);
    }
}
