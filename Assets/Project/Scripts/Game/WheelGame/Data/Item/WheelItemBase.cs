using UnityEngine;

namespace Project.Scripts.Game.WheelGame.Data.Item
{
    public class WheelItemBase : IWheelItem
    {
        public ItemType Type { get; }
        public Sprite Sprite { get; }
        public ItemQuality Quality { get; }
        public WheelItemBase(ItemType type, Sprite texture, ItemQuality quality)
        {
            Type = type;
            Sprite = texture;
            Quality = quality;
        }
    }
}