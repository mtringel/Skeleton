using System;

namespace TopTal.JoggingApp.Service.Api.Helpers
{
    internal class ServiceScopeWithResult<TResult> : ServiceScope
    {
        public TResult Result { get; private set; }

        public ServiceScopeWithResult(Action onComplete, Action onDispose, TResult result)
            : base(onComplete, onDispose)
        {
            this.Result = result;
        }
    }



}