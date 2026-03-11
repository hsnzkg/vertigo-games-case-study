using System;
using Project.Scripts.Game.WheelGame.Data.Item;
using UnityEngine;

namespace Project.Scripts.Game.WheelGame.Data.Provider
{
    [Serializable]
    public struct WheelItemEntry
    {
        public ItemType Type;
        public Sprite Sprite;
        public ItemQuality Quality;
        public int[] ProvidableAmounts;
    }


    [Serializable]
    public struct WheelZone
    {
        public WheelZoneType Type;
        public WheelItemEntry[] Entries;
    }
}