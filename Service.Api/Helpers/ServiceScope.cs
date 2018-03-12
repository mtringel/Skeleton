using System;

namespace TopTal.JoggingApp.Service.Api.Helpers
{
    internal class ServiceScope : IDisposable
    {
        private Action OnComplete;

        private Action OnDispose;

        public ServiceScope(Action onComplete, Action onDispose)
        {
            this.OnComplete = onComplete;
            this.OnDispose = onDispose;
        }

        /// <summary>
        /// Completes transactions (commit)
        /// </summary>
        public void Complete()
        {
            if (this.OnComplete != null)
                OnComplete();
        }

        /// <summary>
        /// Disposes transactions (rollbacks if Commit() was not called)
        /// </summary>
        public void Dispose()
        {
            if (this.OnDispose != null)
                OnDispose();
        }
    }



}