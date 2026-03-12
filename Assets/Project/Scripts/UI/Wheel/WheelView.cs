using System;
using DG.Tweening;
using Project.Scripts.Game.WheelGame.Data.Provider;
using Project.Scripts.UI.Core;
using Project.Scripts.UI.WheelItem;
using Project.Scripts.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Scripts.UI.Wheel
{
    public class WheelView : ViewBase
    {
        [SerializeField]
        private WheelItemSystem m_itemPrefab;

        [SerializeField]
        private RectTransform m_wheelRect;

        [SerializeField]
        private Image m_wheelImage;

        [SerializeField]
        private TextMeshProUGUI m_wheelTitle;

        [SerializeField]
        private Button m_spinButton;

        [SerializeField]
        private Button m_withdrawButton;
        
        [SerializeField]
        private int m_itemCount;

        [SerializeField]
        private float m_radius;

        [SerializeField]
        private float m_rotateDuration;

        [SerializeField]
        private Ease m_rotateEase;

        [SerializeField]
        private int m_maxExtraTurnCount;

        [SerializeField]
        private WheelZoneBlock[] m_zoneBlocks;

        private WheelItemSystem[] m_itemTargets;
        private Vector2 m_itemAABBMag;
        private Tween m_rotateTween;
        private float m_currentAngle;
        public event Action SpinComplete;
        public event Action SpinPress;
        public event Action WithdrawPress;

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
            m_itemTargets = new WheelItemSystem[m_itemCount];
            CreateItemElements();
            m_currentAngle = m_wheelRect.localEulerAngles.z;
            FitItemTargets();
        }

        protected override void OnEnable()
        {
            m_spinButton.onClick.AddListener(OnSpinPressed);
            m_withdrawButton.onClick.AddListener(OnWithdrawPressed);
        }

        protected override void OnDisable()
        {
            m_spinButton.onClick.RemoveListener(OnSpinPressed);
            m_withdrawButton.onClick.RemoveListener(OnWithdrawPressed);
        }

        protected override void OnDestroy()
        {
            if (m_rotateTween is { active: true })
            {
                m_rotateTween.Kill();
            }
        }

        public void SetButtonInteractivity(bool value)
        {
            m_spinButton.interactable = value;
        }

        public void SetWithDrawButtonInteractivity(bool value)
        {
            m_withdrawButton.interactable = value;
        }

        private void OnSpinPressed()
        {
            SpinPress?.Invoke();
        }

        private void OnWithdrawPressed()
        {
            WithdrawPress?.Invoke();
        }

        private void CreateItemElements()
        {
            for (int i = 0; i < m_itemCount; i++)
            {
                WheelItemSystem itemElement = Instantiate(m_itemPrefab, m_wheelRect.transform);
                RectTransform itemRect = itemElement.View.Root;

                Vector3 lossy = itemRect.lossyScale;
                Vector2 size = itemRect.rect.size;
                m_itemAABBMag = new Vector2(size.x * lossy.x, size.y * lossy.y);

                m_itemTargets[i] = itemElement;
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

                m_itemTargets[i].View.Root.anchoredPosition = center + new Vector2(x, y);

                float rotation = angle + 270f;
                m_itemTargets[i].View.Root.localRotation = Quaternion.Euler(0, 0, rotation);
            }
        }

        private WheelZoneBlock GetZoneBlock(WheelZoneType type)
        {
            foreach (WheelZoneBlock block in m_zoneBlocks)
            {
                if (block.Zone == type) return block;
            }

            return default;
        }

        private void ChangeWheelImage(Sprite image)
        {
            m_wheelImage.sprite = image;
        }

        private void ChangeWheelTitle(string prefix)
        {
            m_wheelTitle.text = $"{prefix} Spin";
        }

        public void Rotate(float targetDegree, int extraSpinCount = -1, SpinDirection direction = SpinDirection.Clockwise)
        {
            if (m_rotateTween is { active: true })
            {
                m_rotateTween.Kill();
            }

            if (extraSpinCount == -1)
            {
                extraSpinCount = Random.Range(1, m_maxExtraTurnCount + 1);
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
                    m_currentAngle = finalAngle.NormalizeAngle();
                    m_wheelRect.localEulerAngles = new Vector3(0, 0, m_currentAngle);
                    SpinComplete?.Invoke();
                });
            m_rotateTween.Play();
        }

        public void Rotate(int index, int extraSpinCount = -1, SpinDirection direction = SpinDirection.Clockwise)
        {
            float targetDegree = 360f / m_itemTargets.Length * index;
            Rotate(targetDegree, extraSpinCount, direction);
        }

        public void ChangeItem(int index, WheelItemResult wheelItemResult)
        {
            m_itemTargets[index]
                .Controller
                .ChangeItem(wheelItemResult);
        }

        public void ChangeWheelZone(WheelZoneType zoneType)
        {
            WheelZoneBlock block = GetZoneBlock(zoneType);
            ChangeWheelImage(block.Sprite);
            ChangeWheelTitle(block.ZonePrefix);
        }
    }
}