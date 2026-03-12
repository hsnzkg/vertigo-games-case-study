using Project.Scripts.UI.Core;

namespace Project.Scripts.UI.ZoneIndicator
{
    public class ZoneIndicatorSystem : SystemBase<ZoneIndicatorModel,ZoneIndicatorView, ZoneIndicatorController>
    {
        protected override ZoneIndicatorModel CreateModel()
        {
            return new ZoneIndicatorModel();
        }

        protected override ZoneIndicatorController CreateController(ZoneIndicatorView view, ZoneIndicatorModel model)
        {
            return new ZoneIndicatorController(view, model);
        }
    }
}
