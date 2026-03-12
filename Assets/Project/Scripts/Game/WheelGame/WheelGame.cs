    using EventBus.Runtime;
    using Project.Scripts.EventBus.Events.Wheel.Game;
    using Project.Scripts.EventBus.Events.Wheel.UI;
    using Project.Scripts.EventBus.Events.Bag;
    using Project.Scripts.Game.WheelGame.Data.Item;
    using Project.Scripts.Game.WheelGame.Data.Provider;
    using Project.Scripts.Storage;
    using Storage.Runtime;
    using UnityEngine;

    namespace Project.Scripts.Game.WheelGame
    {
        public class WheelGame : MonoBehaviour
        {
            private int m_selectedIndex;
            private int m_currentZoneIndex;
            private IWheelItemCollectionProvider m_provider;
            private IQualityProgressCalculator m_qualityProcessor;
            private WheelZoneType m_currentZoneType;
            private WheelItemResult[] m_wheelBag;
            private WheelItemResult m_currentSelected;
            private EventBind<ESpinPressed> m_spinBind;
            private EventBind<ESpinCompleted> m_spinCompleted;
            private EventBind<EGiveUp> m_giveUpBind;
 
            private void Awake()
            {
                Initialize();
            }
 
            private void Start()
            {
                PrepareGame();
            }
 
            private void OnEnable()
            {
                EventBus<ESpinPressed>.Register(m_spinBind);
                EventBus<ESpinCompleted>.Register(m_spinCompleted);
                EventBus<EGiveUp>.Register(m_giveUpBind);
            }
 
            private void OnDisable()
            {
                EventBus<ESpinPressed>.Unregister(m_spinBind);
                EventBus<ESpinCompleted>.Unregister(m_spinCompleted);
                EventBus<EGiveUp>.Unregister(m_giveUpBind);
            }

            private void Initialize()
            {
                m_currentZoneIndex = 0;
                m_currentZoneType = WheelZoneType.DEFAULT;
                m_spinBind = new EventBind<ESpinPressed>(StartGame);
                m_spinCompleted = new EventBind<ESpinCompleted>(OnSpinCompleted);
                m_giveUpBind = new EventBind<EGiveUp>(OnGiveUp);
                m_provider = Storage<GameplayStorage>.Instance.WheelItemCollectionProvider;
                m_qualityProcessor = new WheelGameQualityProcessor();
            }

            private void OnGiveUp(EGiveUp obj)
            {
                m_currentZoneIndex = 0;
                m_currentZoneType = WheelZoneType.DEFAULT;
                PrepareGame();
            }

            private void PrepareGame()
            {
                ItemQuality quality = m_qualityProcessor.CalculateQuality(m_currentZoneIndex);
                m_wheelBag = m_provider.Provide(m_currentZoneType, quality);
                EventBus<EPrepareGame>.Raise(new EPrepareGame(m_wheelBag,m_currentZoneType,m_currentZoneIndex));
            }

            private void StartGame()
            { 
                m_selectedIndex = Random.Range(0, m_wheelBag.Length);
                m_currentSelected = m_wheelBag[m_selectedIndex];
                EventBus<EGameStart>.Raise(new EGameStart(m_selectedIndex));
            }

            private void OnSpinCompleted()
            {
                if (m_currentSelected.Item.Type == ItemType.Bomb)
                {
                    EventBus<EBombExplode>.Raise(new EBombExplode());
                }
                else
                {
                    EventBus<EAddItem>.Raise(new EAddItem(m_currentSelected.Item, m_currentSelected.Amount));
                    m_currentZoneIndex++;
                    m_currentZoneType = GetZoneTypeFromIndex(m_currentZoneIndex);
                    PrepareGame();
                }
            }

            private static WheelZoneType GetZoneTypeFromIndex(int index)
            {
                WheelZoneType type;
                bool safeFlag = (index+1) % 5 == 0;
                bool goldFlag = (index+1) % 30 == 0;
                if (goldFlag)
                {
                    type = WheelZoneType.SUPER;
                }
                else if (safeFlag)
                {
                    type = WheelZoneType.SAFE;
                }
                else
                {
                    type = WheelZoneType.DEFAULT;
                }   
                return type;
            }
        }
    }