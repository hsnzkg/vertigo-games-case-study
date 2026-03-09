using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Addressable
{
    public class AddressableOperation<T>
    {
        private UniTask<T> m_task;
        private readonly Func<IProgress<float>, CancellationToken, UniTask<T>> m_factory;
        private readonly CancellationToken m_cancellationToken;
        private bool m_isStarted;

        private Action<T> m_onComplete;
        private Action<float> m_onProgress;
        private Action<string> m_onError;

        private readonly Progress<float> m_progressReporter;
        private readonly string m_operationContext;

        public AddressableOperation(Func<IProgress<float>, CancellationToken, UniTask<T>> factory, string operationContext, CancellationToken cancellationToken = default)
        {
            m_operationContext = operationContext;
            m_progressReporter = new Progress<float>(ReportProgress);
            m_factory = factory;
            m_cancellationToken = cancellationToken;
        }
        
        public AddressableOperation<T> Start()
        {
            if (!m_isStarted)
            {
                m_isStarted = true;
                m_task = RunAsync(m_factory, m_cancellationToken);
            }
            return this;
        }
        
        public AddressableOperation<T> OnComplete(Action<T> callback)
        {
            m_onComplete = callback;
            return this;
        }
        
        public AddressableOperation<T> OnProgress(Action<float> callback)
        {
            m_onProgress = callback;
            return this;
        }
        
        public AddressableOperation<T> OnError(Action<string> callback)
        {
            m_onError = callback;
            return this;
        }
        
        public UniTask<T> ToUniTask()
        {
            Start();
            return m_task;
        }
        
        public UniTask<T>.Awaiter GetAwaiter()
        {
            Start();
            return m_task.GetAwaiter();
        }

        private async UniTask<T> RunAsync(Func<IProgress<float>, CancellationToken, UniTask<T>> factory, CancellationToken cancellationToken)
        {
            try
            {
                Debug.Log($"[Addressable Manager] {m_operationContext} — Starting operation.");

                T result = await factory(m_progressReporter, cancellationToken);

                Debug.Log($"[Addressable Manager] {m_operationContext} — Completed successfully.");
                m_onComplete?.Invoke(result);

                return result;
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning($"[Addressable Manager] {m_operationContext} — Operation was cancelled.");
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