using UnityEngine;

namespace Project.Scripts.UI.Core
{
    public abstract class ViewBase : MonoBehaviour, IView
    {
        [SerializeField]
        protected RectTransform Root;

        public virtual void Open()
        {
        }

        public virtual void OpenImmediately()
        {
            if (Root != null)
            {
                Root.gameObject.SetActive(true);
            }
        }

        public virtual void Close()
        {
        }

        public virtual void CloseImmediately()
        {
            if (Root != null)
            {
                Root.gameObject.SetActive(false);
            }
        }
    }
}