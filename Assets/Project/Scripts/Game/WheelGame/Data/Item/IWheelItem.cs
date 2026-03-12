using UnityEngine;

namespace Project.Scripts.Game.WheelGame.Data.Item
{
    public interface IWheelItem
    {
        int Id { get; }
        Sprite Sprite { get; }
        ItemType Type { get; }
        ItemQuality Quality { get; }
    }
}