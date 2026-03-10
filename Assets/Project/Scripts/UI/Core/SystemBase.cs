using UnityEngine;

namespace Project.Scripts.UI.Core
{
    public abstract class SystemBase<TView, TModel, TController> : MonoBehaviour where TView : ViewBase where TModel : IModel where TController : ControllerBase<TView, TModel>
    {
        [SerializeField]
        private TView m_view;

        protected TController Controller { get; private set; }
        protected TModel Model { get; private set; }

        protected virtual void Awake()
        {
            Build();
        }

        protected virtual void Build()
        {
            Model = CreateModel();
            Controller = CreateController(m_view, Model);
            Controller.Initialize();
        }

        protected abstract TModel CreateModel();
        protected abstract TController CreateController(TView view, TModel model);
    }
}