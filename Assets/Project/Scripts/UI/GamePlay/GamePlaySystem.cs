using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.GamePlay
{
    public class GamePlaySystem : SystemBase<GamePlayModel, GamePlayView, GamePlayController>
    {
        protected override GamePlayModel CreateModel()
        {
            return new GamePlayModel();
        }

        protected override GamePlayController CreateController(GamePlayView view, GamePlayModel model)
        {
            return new GamePlayController(view, model);
        }
    }
}
