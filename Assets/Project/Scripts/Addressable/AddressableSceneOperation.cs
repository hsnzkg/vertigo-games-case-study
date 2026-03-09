using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Project.Scripts.Addressable
{
    public class AddressableSceneOperation
    {
        private UniTask<SceneInstance> m_task;
        private readonly Func<AddressableSceneOperation, IProgress<float>, CancellationToken, UniTask<SceneInstance>> m_factory;
        private readonly CancellationToken m_cancellationToken;
        private bool m_isStarted;

        private Action<SceneInstance> m_onComplete;
        private Action<float> m_onProgress;
        private Action<string> m_onError;
        private Action m_onBeforeActivation;
        private Action m_onAfterActivation;

        private readonly Progress<float> m_progressReporter;
        private readonly string m_operationContext;

        public AddressableSceneOperation(Func<AddressableSceneOperation, IProgress<float>, CancellationToken, UniTask<SceneInstance>> factory, string operationContext, CancellationToken cancellationToken = default)
        {
            m_operationContext = operationContext;
            m_progressReporter = new Progress<float>(ReportProgress);
            m_factory = factory;
            m_cancellationToken = cancellationToken;
        }
        
        public AddressableSceneOperation Start()
        {
            if (!m_isStarted)
            {
                m_isStarted = true;
                m_task = RunAsync(m_factory, m_cancellationToken);
            }
            return this;
        }
        
        public AddressableSceneOperation OnComplete(Action<SceneInstance> callback)
        {
            m_onComplete = callback;
            return this;
        }

        public AddressableSceneOperation OnProgress(Action<float> callback)
        {
            m_onProgress = callback;
            return this;
        }

        public AddressableSceneOperation OnError(Action<string> callback)
        {
            m_onError = callback;
            return this;
        }

        public AddressableSceneOperation OnBeforeActivation(Action callback)
        {
            m_onBeforeActivation = callback;
            return this;
        }


        public AddressableSceneOperation OnAfterActivation(Action callback)
        {
            m_onAfterActivation = callback;
            return this;
        }

        internal void TriggerBeforeActivation()
        {
            m_onBeforeActivation?.Invoke();
        }

        internal void TriggerAfterActivation()
        {
            m_onAfterActivation?.Invoke();
        }

        public UniTask<SceneInstance> ToUniTask()
        {
            Start();
            return m_task;
        }

        public UniTask<SceneInstance>.Awaiter GetAwaiter()
        {
            Start();
            return m_task.GetAwaiter();
        }

        private async UniTask<SceneInstance> RunAsync(Func<AddressableSceneOperation, IProgress<float>, CancellationToken, UniTask<SceneInstance>> factory, CancellationToken cancellationToken)
        {
            try
            {
                Debug.Log($"[Addressable Manager] {m_operationContext} — Starting scene load.");

                SceneInstance result = await factory(this, m_progressReporter, cancellationToken);

                Debug.Log($"[Addressable Manager] {m_operationContext} — Scene loaded successfully.");
                m_onComplete?.Invoke(result);

                return result;
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning($"[Addressable Manager] {m_operationContext} — Scene load was cancelled.");
                throw;
            }
            catch (Exception ex)
            {
                string errorMessage = $"[Addressable Manager] {m_operationContext} — Error: {ex.Message}";
                Debug.LogError(errorMessage);
                m_onError?.Invoke(ex.Message);

                throw;
            }
        }

        private void ReportProgress(float progress)
        {
            m_onProgress?.Invoke(progress);
        }
    }
}