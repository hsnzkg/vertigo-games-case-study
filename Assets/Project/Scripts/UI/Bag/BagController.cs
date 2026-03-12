using System.Linq;
using Project.Scripts.UI.BagItem;
using Project.Scripts.UI.Core;
using UnityEngine;
using Project.Scripts.EventBus.Events.Bag;
using EventBus.Runtime;
using Project.Scripts.Game.WheelGame.Data.Item;

namespace Project.Scripts.UI.Bag
{
    public class BagController : ControllerBase<BagView, BagModel>
    {
        private readonly EventBind<EAddItem> m_addItemBind;
 
        public BagController(BagView view, BagModel model) : base(view, model)
        {
            m_addItemBind = new EventBind<EAddItem>(OnAddItem);
        }

        public override void Enable()
        {
            EventBus<EAddItem>.Register(m_addItemBind);
        }

        public override void Disable()
        {
            EventBus<EAddItem>.Unregister(m_addItemBind);
        }

        private void OnAddItem(EAddItem obj)
        {
            AddItem(obj.Item,obj.Amount);
        }

        private void AddItem(IWheelItem wheelItem, int amount)
        {
            int id = wheelItem.Id;
            Sprite image = wheelItem.Sprite;
            
            BagItemSystem existingItem = Model.Items.FirstOrDefault(item => item.Model.Id == id);

            if (existingItem != null)
            {
                existingItem.Model.Amount.Value += amount;
            }
            else
            {
                BagItemSystem newItem = View.CreateBagItem();
                newItem.Controller.Initialize(id, image, amount);
                Model.Items.Add(newItem);
            }
        }

        public void ClearItems()
        {
            View.ClearContent();
            Model.Items.Clear();
        }
    }
}
