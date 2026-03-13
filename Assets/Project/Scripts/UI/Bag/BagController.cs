using System.Linq;
using Project.Scripts.UI.BagItem;
using Project.Scripts.UI.Core;
using UnityEngine;
using Project.Scripts.EventBus.Events.Bag;
using EventBus.Runtime;
using Project.Scripts.EventBus.Events.Wheel;
using Project.Scripts.EventBus.Events.Wheel.Game;
using Project.Scripts.Game.WheelGame.Data.Item;

namespace Project.Scripts.UI.Bag
{
    public class BagController : ControllerBase<BagView, BagModel>
    {
        private readonly EventBind<EAddItem> m_addItemBind;
        private readonly EventBind<EGiveUp> m_giveUpBind;
        private readonly EventBind<EWithdraw> m_withdrawBind;
 
        public BagController(BagView view, BagModel model) : base(view, model)
        {
            m_addItemBind = new EventBind<EAddItem>(OnAddItem);
            m_giveUpBind = new EventBind<EGiveUp>(OnGiveUp);
            m_withdrawBind = new EventBind<EWithdraw>(OnWithdraw);
        }

        public override void Enable()
        {
            EventBus<EAddItem>.Register(m_addItemBind);
            EventBus<EGiveUp>.Register(m_giveUpBind);
            EventBus<EWithdraw>.Register(m_withdrawBind);
        }

        public override void Disable()
        {
            EventBus<EAddItem>.Unregister(m_addItemBind);
            EventBus<EGiveUp>.Unregister(m_giveUpBind);
            EventBus<EWithdraw>.Unregister(m_withdrawBind);
        }

        private void OnGiveUp(EGiveUp obj)
        {
            ClearItems();
        }

        private void OnWithdraw(EWithdraw obj)
        {
            ClearItems();
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
