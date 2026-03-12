using Project.Scripts.UI.Core;
using UnityEngine;

namespace Project.Scripts.UI.BagItem
{
    public class BagItemModel : IModel
    {
        public int Id { get; set; }
        public readonly Observable<int> Amount = new(0);
        public readonly Observable<Sprite> Icon = new(null);
    }
}
