using UnityEngine;

namespace Project.Scripts.UI.Wheel.Data.Item
{
    public interface IWheelItem
    {
        Texture2D Texture { get; }
        int Id { get; }
    }
}