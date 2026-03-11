using UnityEngine;

namespace Project.Scripts.Game.WheelGame.Data.Item
{
    public interface IWheelItem
    {
        Texture2D Texture { get; }
        int Id { get; }
    }
}