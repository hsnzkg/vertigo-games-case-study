using EventBus.Runtime;
using FiniteStateMachine.Runtime;
using FiniteStateMachine.Runtime.RuntimeMode;
using Project.Scripts.ApplicationState.States;
using Project.Scripts.Utility;
using Storage.Runtime;
using UnityEngine;

namespace Project.Scripts.ApplicationState
{
    public class ClientStateManager : IApplicationStateManager
    {
        private StateMachine m_clientStateMachine;
        public void Initialize()
        {
            EventBusCenter.Initialize();
            StorageCenter.Initialize();
            MonoBehaviourBridge.Create();
            
            Application.wantsToQuit += OnApplicationWantsToQuit;
            Application.quitting += OnApplicationQuitting;
            
            Preload preloadState = new();
            Gameplay gameplay = new();
            WantsToQuit wantsToQuit = new();
            Quit quitState = new();
            
            m_clientStateMachine = new StateMachine(new ManualMode());
            m_clientStateMachine.AddState(preloadState);
            m_clientStateMachine.AddState(gameplay);
            m_clientStateMachine.AddState(wantsToQuit);
            m_clientStateMachine.AddState(quitState);
            RequestStateChange<Preload>();
        }

        private bool OnApplicationWantsToQuit()
        {
            Application.wantsToQuit -= OnApplicationWantsToQuit;
            RequestStateChange<WantsToQuit>();
            return true;
        }

        private void OnApplicationQuitting()
        {
            Application.quitting -= OnApplicationQuitting;
            RequestStateChange<Quit>();
        }

        public void RequestStateChange<T>()
        {   
            m_clientStateMachine.ChangeState<T>();
        }
    }
}