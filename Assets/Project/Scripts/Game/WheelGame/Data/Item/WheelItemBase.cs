using UnityEngine;

namespace Project.Scripts.Game.WheelGame.Data.Item
{
    public class WheelItemBase : IWheelItem
    {
        public int Id { get; }
        public ItemType Type { get; }
        public Sprite Sprite { get; }
        public ItemQuality Quality { get; }

        public WheelItemBase(int id, ItemType type, Sprite texture, ItemQuality quality)
        {
            Id = id;
            Type = type;
            Sprite = texture;
            Quality = quality;
        }
    }
}