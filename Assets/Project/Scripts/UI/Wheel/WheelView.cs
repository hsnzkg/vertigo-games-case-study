using System;
using Project.Scripts.UI.Core;
using UnityEngine;

namespace Project.Scripts.UI.Wheel
{
    public class WheelView : ViewBase
    {
        [SerializeField]
        private RectTransform m_wheelRect;

        [SerializeField]
        private RectTransform[] m_itemTargets;

        [SerializeField]
        private float m_radius;

        private Vector2 m_itemAABBMag;

        private void Awake()
        {
            Vector3 lossy = m_itemTargets[0].lossyScale;
            Vector2 size = m_itemTargets[0].rect.size;
            m_itemAABBMag = new Vector2(size.x * lossy.x, size.y * lossy.y);
        }

        private void OnDrawGizmos()
        {
            if (m_wheelRect == null || m_itemTargets == null || m_itemTargets.Length == 0) return;

            Gizmos.color = Color.yellow;

            Vector3 center = m_wheelRect.position;

            int count = m_itemTargets.Length;
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = angleStep * i;
                float rad = angle * Mathf.Deg2Rad;

                float x = Mathf.Cos(rad) * m_radius;
                float y = Mathf.Sin(rad) * m_radius;

                Vector3 worldPos = center + m_wheelRect.TransformVector(new Vector3(x, y, 0f));

                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(worldPos, m_itemAABBMag);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(center, worldPos);
            }
        }

        public void FitItemTargets()
        {
            if (m_itemTargets == null || m_itemTargets.Length == 0) return;

            Vector2 center = m_wheelRect.anchoredPosition;
            int count = m_itemTargets.Length;
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = angleStep * i;
                float rad = angle * Mathf.Deg2Rad;

                float x = Mathf.Cos(rad) * m_radius;
                float y = Mathf.Sin(rad) * m_radius;

                m_itemTargets[i].anchoredPosition = center + new Vector2(x, y);
                
                float rotation = angle + 180f;
                m_itemTargets[i].localRotation = Quaternion.Euler(0, 0, rotation);
                
                //m_itemTargets[i].localRotation = Quaternion.Euler(0, 0, angle);
                //m_itemTargets[i].localRotation = Quaternion.identity;
            }
        }
    }
}
