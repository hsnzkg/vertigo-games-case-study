using UnityEngine;

namespace Project.Scripts.UI.Wheel.Data.Item
{
    public class WheelItemBase : IWheelItem
    {
        public int Id { get; }
        public Texture2D Texture { get; }

        public WheelItemBase(int id, Texture2D texture)
        {
            Id = id;
            Texture = texture;
        }
    }
}