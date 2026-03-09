using EventBus.Runtime;
using Project.Scripts.EventBus.Events.MonoBehaviour;
using UnityEngine;

namespace Project.Scripts.Utility
{
    public class MonoBehaviourBridge : MonoBehaviourSingleton<MonoBehaviourBridge>
    {
        protected override void OnAwake()
        {
            MakePersistent();
        }

        protected override void OnEnabled()
        {
        }

        protected override void OnDisabled()
        {
        }

        private void Update()
        {
            EventBus<EUpdate>.Raise(new EUpdate(Time.deltaTime));
        }

        private void FixedUpdate()
        {
            EventBus<EFixedUpdate>.Raise(new EFixedUpdate(Time.fixedDeltaTime));
        }

        private void LateUpdate()
        {
            EventBus<ELateUpdate>.Raise(new ELateUpdate(Time.deltaTime));
        }
    }
}