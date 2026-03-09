using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Addressable
{
    public static class AddressableManager
    {
        public static AddressableOperation<T> LoadAssetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            return new AddressableOperation<T>(async (progress, ct) =>
                {
                    AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);

                    while (!handle.IsDone)
                    {
                        progress.Report(handle.PercentComplete);
                        await UniTask.Yield(PlayerLoopTiming.Update, ct);
                    }

                    progress.Report(1f);

                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        throw new Exception($"Failed to load asset '{key}': {handle.OperationException?.Message}");
                    }

                    return handle.Result;
                }, $"LoadAssetAsync<{typeof(T).Name}>(\"{key}\")", cancellationToken);
        }

        public static AddressableOperation<List<T>> LoadAssetsAsync<T>(IEnumerable<string> keys, CancellationToken cancellationToken = default)
        {
            return new AddressableOperation<List<T>>(async (progress, ct) =>
                {
                    List<string> keyList = new(keys);
                    List<T> results = new(keyList.Count);
                    int totalCount = keyList.Count;

                    if (totalCount == 0)
                    {
                        Debug.LogWarning("[Addressable Manager] LoadAssetsAsync — Empty key list provided.");
                        progress.Report(1f);
                        return results;
                    }

                    for (int i = 0; i < totalCount; i++)
                    {
                        ct.ThrowIfCancellationRequested();

                        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyList[i]);

                        while (!handle.IsDone)
                        {
                            float itemProgress = handle.PercentComplete;
                            float overallProgress = (i + itemProgress) / totalCount;
                            progress.Report(overallProgress);
                            await UniTask.Yield(PlayerLoopTiming.Update, ct);
                        }

                        if (handle.Status == AsyncOperationStatus.Failed)
                        {
                            throw new Exception($"Failed to load asset '{keyList[i]}': {handle.OperationException?.Message}");
                        }

                        results.Add(handle.Result);
                    }

                    progress.Report(1f);
                    return results;
                }, $"LoadAssetsAsync<{typeof(T).Name}>({string.Join(", ", keys)})", cancellationToken);
        }

        public static AddressableOperation<IList<T>> LoadAssetsByKeyAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            return new AddressableOperation<IList<T>>(async (progress, ct) =>
                {
                    AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(key, null);

                    while (!handle.IsDone)
                    {
                        progress.Report(handle.PercentComplete);
                        await UniTask.Yield(PlayerLoopTiming.Update, ct);
                    }

                    progress.Report(1f);

                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        throw new Exception($"Failed to load assets by key '{key}': {handle.OperationException?.Message}");
                    }

                    Debug.Log($"[Addressable Manager] LoadAssetsByKeyAsync<{typeof(T).Name}>(\"{key}\") — Loaded {handle.Result.Count} asset(s).");

                    return handle.Result;
                }, $"LoadAssetsByKeyAsync<{typeof(T).Name}>(\"{key}\")", cancellationToken);
        }
        
        public static AddressableSceneOperation LoadSceneAsync(string key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100, CancellationToken cancellationToken = default)
        {
            return new AddressableSceneOperation(async (operation, progress, ct) =>
                {
                    operation.TriggerBeforeActivation();
                    
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(key, loadMode, false, priority);

                    while (!handle.IsDone)
                    {
                        float percent = handle.PercentComplete;
                        progress.Report(activateOnLoad ? percent * 0.9f : percent);
                        await UniTask.Yield(PlayerLoopTiming.Update, ct);
                    }
                    
                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        throw new Exception($"Failed to load scene '{key}': {handle.OperationException?.Message}");
                    }

                    SceneInstance sceneInstance = handle.Result;

                    if (activateOnLoad)
                    {
                        AsyncOperation activateOp = sceneInstance.ActivateAsync();
                        if (activateOp != null)
                        {
                            while (!activateOp.isDone)
                            {
                                progress.Report(0.9f + (activateOp.progress * 0.1f));
                                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                            }
                        }
                    }
                    progress.Report(1f);
                    if (activateOnLoad)
                    {
                        operation.TriggerAfterActivation();
                    }
                    return sceneInstance;
                }, $"LoadSceneAsync(\"{key}\", {loadMode}, activateOnLoad: {activateOnLoad})", cancellationToken);
        }


        public static void ReleaseAsset<T>(T asset)
        {
            Debug.Log($"[Addressable Manager] Releasing asset of type {typeof(T).Name}.");
            Addressables.Release(asset);
        }
        
        public static async UniTask UnloadSceneAsync(SceneInstance sceneInstance)
        {
            Debug.Log($"[Addressable Manager] Unloading scene '{sceneInstance.Scene.name}'.");

            AsyncOperationHandle<SceneInstance> handle = Addressables.UnloadSceneAsync(sceneInstance);

            while (!handle.IsDone)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (handle.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError($"[Addressable Manager] Failed to unload scene: {handle.OperationException?.Message}");
                throw new Exception($"Failed to unload scene: {handle.OperationException?.Message}");
            }

            Debug.Log("[Addressable Manager] Scene unloaded successfully.");
        }
    }
}