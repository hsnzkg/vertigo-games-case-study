using System.Collections.Generic;
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

        private void Fill(WheelZoneType zone, ItemQuality targetQuality)
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

            List<WheelItemEntry> validEntries = new();
            List<WheelItemEntry> bombEntries = new();
            
            foreach(WheelItemEntry entry in targetZone.Entries)
            {
                if (entry.Type == ItemType.Bomb)
                {
                    bombEntries.Add(entry);
                }
                
                if (entry.Quality == targetQuality && entry.Type != ItemType.Bomb)
                {
                    validEntries.Add(entry);
                }
            }
            
            if (validEntries.Count == 0)
            {
                foreach(WheelItemEntry entry in targetZone.Entries)
                {
                    if (entry.Type != ItemType.Bomb)
                    {
                        validEntries.Add(entry);
                    }
                }

                if (validEntries.Count == 0)
                {
                    validEntries.AddRange(targetZone.Entries);
                }
            }

            bool requireBomb = zone != WheelZoneType.SAFE && zone != WheelZoneType.SUPER;
            int bombIndex = -1;
            
            if (requireBomb && bombEntries.Count > 0 && m_count > 0)
            {
                bombIndex = m_random.Next(0, m_count);
            }

            for (int i = 0; i < m_count; i++)
            {
                if (i == bombIndex)
                {
                    WheelItemEntry bombEntry = bombEntries[m_random.Next(0, bombEntries.Count)];
                    m_itemBuffer[i] = new WheelItemResult(new WheelItemBase(bombEntry.Id,bombEntry.Type, bombEntry.Sprite, bombEntry.Quality), bombEntry.ProvidableAmounts[m_random.Next(0, bombEntry.ProvidableAmounts.Length)]);
                }
                else
                {
                    WheelItemEntry entry = validEntries[m_random.Next(0, validEntries.Count)];
                    m_itemBuffer[i] = new WheelItemResult(new WheelItemBase(entry.Id,entry.Type, entry.Sprite, entry.Quality), entry.ProvidableAmounts[m_random.Next(0, entry.ProvidableAmounts.Length)]);
                }
            }
        }

        public WheelItemResult[] Provide(WheelZoneType zoneType, ItemQuality targetQuality = ItemQuality.Common, int seed = -1)
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
            Fill(zoneType, targetQuality);
            
            return m_itemBuffer;
        }
    }
}