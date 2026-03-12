using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.RevivePanel
{
    public class RevivePanelSystem : SystemBase<RevivePanelModel, RevivePanelView, RevivePanelController>
    {
        protected override RevivePanelModel CreateModel()
        {
            return new RevivePanelModel();
        }

        protected override RevivePanelController CreateController(RevivePanelView view, RevivePanelModel model)
        {
            return new RevivePanelController(view, model);
        }
    }
}
