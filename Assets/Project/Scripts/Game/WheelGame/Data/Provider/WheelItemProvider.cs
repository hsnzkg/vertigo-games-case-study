using Project.Scripts.Game.WheelGame.Data.Item;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.Game.WheelGame.Data.Provider
{
    [CreateAssetMenu(fileName = "WheelItemProvider", menuName = "Project/WheelItemProvider", order = 0)]
    public class WheelItemProvider : ScriptableObject, IWheelItemCollectionProvider
    {
        [SerializeField]
        private WheelZone[] m_zones;
        
        [SerializeField]
        private int m_count;

        [SerializeField]
        private int m_seed = -1;

        private WheelItemResult[] m_itemBuffer;
        private Random m_random;


        public WheelItemProvider()
        {
            m_random = new Random(m_seed);
            m_itemBuffer = new WheelItemResult[m_count];
        }

        private void OnValidate()
        {
            m_itemBuffer = new WheelItemResult[m_count];
            m_random = new Random(m_seed);
        }

        private void Fill(WheelZoneType zone)
        {
            if (m_zones == null || m_zones.Length == 0) return;
            if (m_itemBuffer == null || m_itemBuffer.Length != m_count)
            {
                m_itemBuffer = new WheelItemResult[m_count];
            }

            WheelZone targetZone = default;
            bool found = false;

            foreach (WheelZone z in m_zones)
            {
                if (z.Type == zone)
                {
                    targetZone = z;
                    found = true;
                    break;
                }
            }

            if (!found || targetZone.Entries == null || targetZone.Entries.Length == 0)
            {
                Debug.LogWarning($"[WheelItemProvider] Zone {zone} not found or has no entries!");
                return;
            }

            for (int i = 0; i < m_count; i++)
            {
                int randomIndex = m_random.Next(0, targetZone.Entries.Length);
                WheelItemEntry entry = targetZone.Entries[randomIndex];
                m_itemBuffer[i] = new WheelItemResult(new WheelItemBase(entry.Type, entry.Sprite,entry.Quality),entry.ProvidableAmounts[m_random.Next(0,entry.ProvidableAmounts.Length)]);
            }
        }

        public WheelItemResult[] Provide(WheelZoneType zoneType, int seed = -1)
        {
            if (seed == -1)
            {
                m_seed = UnityEngine.Random.Range(0, int.MaxValue);
            }
            else
            {
                m_seed = seed;
            }
            
            m_random = new Random(m_seed);
            Fill(zoneType);
            
            return m_itemBuffer;
        }
    }
}