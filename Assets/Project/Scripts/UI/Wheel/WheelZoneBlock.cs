using System;
using Project.Scripts.Game.WheelGame.Data.Provider;
using UnityEngine;

namespace Project.Scripts.UI.Wheel
{
    [Serializable]
    public struct WheelZoneBlock
    {
        public WheelZoneType Zone;
        public string ZonePrefix;
        public Sprite Sprite;
    }
}