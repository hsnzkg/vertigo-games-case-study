using System;
using DG.Tweening;
using Project.Scripts.UI.Core;
using Project.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Scripts.UI.Wheel
{
    public class WheelView : ViewBase
    {
        [SerializeField]
        private GameObject m_itemPrefab;

        [SerializeField]
        private RectTransform m_wheelRect;

        [SerializeField]
        private Button m_spinButton;

        [SerializeField]
        private int m_itemCount;

        [SerializeField]
        private RectTransform[] m_itemTargets;

        [SerializeField]
        private float m_radius;

        [SerializeField]
        private float m_rotateDuration;

        [SerializeField]
        private Ease m_rotateEase;

        [SerializeField]
        private int m_maxTurnCount;

        private Vector2 m_itemAABBMag;
        private Tween m_rotateTween;
        private float m_currentAngle;
        public event Action RotateComplete;
        public event Action SpinPress;

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

        protected override void Initialize()
        {
            m_itemTargets = new RectTransform[m_itemCount];
            CreateItemElements();
            m_currentAngle = m_wheelRect.localEulerAngles.z;
            FitItemTargets();
        }

        protected override void OnEnable()
        {
            m_spinButton.onClick.AddListener(OnSpinPressed);
        }

        protected override void OnDisable()
        {
            m_spinButton.onClick.RemoveListener(OnSpinPressed);
        }

        protected override void OnDestroy()
        {
            if (m_rotateTween is { active: true })
            {
                m_rotateTween.Kill();
            }
        }

        private void SetButtonInteractivity(bool value)
        {
            m_spinButton.interactable = value;
        }
        
        private void OnSpinPressed()
        {
            SpinPress?.Invoke();
        }
        
        private void CreateItemElements()
        {
            for (int i = 0; i < m_itemCount; i++)
            {
                GameObject itemElement = Instantiate(m_itemPrefab, m_wheelRect.transform);
                RectTransform itemRect = itemElement.GetComponent<RectTransform>();

                Vector3 lossy = itemRect.lossyScale;
                Vector2 size = itemRect.rect.size;
                m_itemAABBMag = new Vector2(size.x * lossy.x, size.y * lossy.y);

                m_itemTargets[i] = itemRect;
            }
        }

        private void FitItemTargets()
        {
            if (m_itemTargets == null || m_itemTargets.Length == 0) return;

            Vector2 center = m_wheelRect.anchoredPosition;
            int count = m_itemTargets.Length;
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = -angleStep * i + 90f;
                float rad = angle * Mathf.Deg2Rad;

                float x = Mathf.Cos(rad) * m_radius;
                float y = Mathf.Sin(rad) * m_radius;

                m_itemTargets[i].anchoredPosition = center + new Vector2(x, y);

                float rotation = angle + 180f;
                m_itemTargets[i].localRotation = Quaternion.Euler(0, 0, rotation);
            }
        }

        public void Rotate(float targetDegree, int extraSpinCount = -1, SpinDirection direction = SpinDirection.Clockwise)
        {
            if (m_rotateTween is { active: true })
            {
                m_rotateTween.Kill();
            }

            if (extraSpinCount == -1)
            {
                extraSpinCount = Random.Range(1, m_maxTurnCount + 1);
                Debug.Log(extraSpinCount);
            }

            float dirSign = (int)direction;

            targetDegree = targetDegree.NormalizeAngle();
            float normalizedCurrent = m_currentAngle.NormalizeAngle();

            float delta = dirSign > 0 ? targetDegree - normalizedCurrent : normalizedCurrent - targetDegree;
            if (delta < 0) delta += 360f;

            float totalRotation = dirSign * (extraSpinCount * 360f + delta);
            float finalAngle = m_currentAngle + totalRotation;

            m_rotateTween = m_wheelRect.DOLocalRotate(new Vector3(0, 0, finalAngle), m_rotateDuration, RotateMode.FastBeyond360)
                .SetEase(m_rotateEase)
                .OnComplete(() =>
                {
                    m_currentAngle = finalAngle;
                    RotateComplete?.Invoke();
                });
            m_rotateTween.Play();
        }

        public void Rotate(int index, int extraSpinCount = -1, SpinDirection direction = SpinDirection.Clockwise)
        {
            float targetDegree = 360f / m_itemTargets.Length * index;
            Rotate(targetDegree, extraSpinCount, direction);
        }
    }
}