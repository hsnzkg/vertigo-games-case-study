using System;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.Runtime;
using Project.Scripts.Addressable;
using Project.Scripts.Game.WheelGame.Data.Provider;
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
            await LoadAssets();
            await LoadScene();
        }

        private UniTask LoadAssets()
        {
            return AddressableManager.LoadAssetAsync<IWheelItemCollectionProvider>("Assets/Project/Data/WheelItemProvider.asset").OnComplete(OnProviderLoaded).ToUniTask();
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

        private void OnProviderLoaded(IWheelItemCollectionProvider obj)
        {
            Storage<GameplayStorage>.Instance.WheelItemCollectionProvider = obj;
        }
    }
}