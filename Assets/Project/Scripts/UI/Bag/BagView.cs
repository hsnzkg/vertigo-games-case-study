using Project.Scripts.UI.BagItem;
using Project.Scripts.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Bag
{
    public class BagView : ViewBase
    {
        [SerializeField]
        private BagItemSystem m_itemPrefab;

        [SerializeField]
        private RectTransform m_content;

        [SerializeField]
        private ScrollRect m_scrollRect;

        [SerializeField]
        private float m_itemOffset;
        
        protected override void Initialize()
        {
            m_content.pivot = new Vector2(0.5f, 1f);
            m_content.anchorMin = new Vector2(0.5f, 1f);
            m_content.anchorMax = new Vector2(0.5f, 1f);
        }

        public BagItemSystem CreateBagItem()
        {
            int index = m_content.childCount;
            BagItemSystem item = Instantiate(m_itemPrefab, m_content);
            
            RectTransform itemRect = item.View.Root;
            itemRect.anchorMin = new Vector2(0.5f, 1f);
            itemRect.anchorMax = new Vector2(0.5f, 1f);
            itemRect.pivot = new Vector2(0.5f, 1f);
            
            itemRect.anchoredPosition = new Vector2(0, -index * m_itemOffset);
            m_content.sizeDelta = new Vector2(m_content.sizeDelta.x, (index + 1) * m_itemOffset);
            return item;
        }

        public void ClearContent()
        {
            foreach (Transform child in m_content)
            {
                Destroy(child.gameObject);
            }
            m_content.sizeDelta = new Vector2(m_content.sizeDelta.x, 0);
        }
    }
}
