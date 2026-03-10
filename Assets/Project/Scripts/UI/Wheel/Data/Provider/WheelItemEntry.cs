using System;
using UnityEngine;

namespace Project.Scripts.UI.Wheel.Data.Provider
{
    [Serializable]
    public struct WheelItemEntry
    {
        public int Id;
        public Texture2D Sprite;
        public int[] ProvidableAmounts;
    }


    [Serializable]
    public struct WheelZone
    {
        public WheelZoneType Type;
        public WheelItemEntry[] Entries;
    }
}