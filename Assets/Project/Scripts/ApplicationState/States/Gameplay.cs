using System;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.Runtime;
using Project.Scripts.Addressable;
using Project.Scripts.Storage;
using Storage.Runtime;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Project.Scripts.ApplicationState.States
{
    public class Gameplay : StateBase
    {
        protected override void OnEnter()
        {
            Storage<GameplayStorage>.Create();
            LoadSequence().Forget();
        }

        protected override void OnExit()
        {
            Storage<GameplayStorage>.DisposeWithCenter();
        }

        private async UniTaskVoid LoadSequence()
        {
            //await LoadAssets();
            await LoadScene();
        }

        private UniTask LoadAssets()
        {
            throw new NotImplementedException();
            //return AddressableManager.LoadAssetAsync<InputManagerSettings>("Assets/FloorRush/Data/InputManagerSettings.asset").OnComplete(OnInputSettingsLoaded).ToUniTask();
        }

        private UniTask LoadScene()
        {
            return AddressableManager.LoadSceneAsync("Assets/Project/Scenes/Addressable/Gameplay.unity")
                .OnBeforeActivation(OnBeforeGameplaySceneActivation)
                .OnComplete(OnGameplaySceneLoaded)
                .Start().ToUniTask();
        }

        private void OnBeforeGameplaySceneActivation()
        {
        }

        private void OnGameplaySceneLoaded(SceneInstance obj)
        {
        }
    }
}