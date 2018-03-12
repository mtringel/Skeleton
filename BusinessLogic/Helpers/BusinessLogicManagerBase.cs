using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopTal.JoggingApp.DataAccess;
using TopTal.JoggingApp.Configuration;
using TopTal.JoggingApp.Security;
using System.Net;
using TopTal.JoggingApp.CallContext;
using TopTal.JoggingApp.Security.Managers;

namespace TopTal.JoggingApp.BusinessLogic.Helpers
{
    /// <summary>
    /// Lifetime: n/a (transient or current request, undetermined, don't rely on internal state)
    /// BusinessLogicManagers should not call each others DataProvider, but can call each other.
    /// Do not add public instance methods here.
    /// </summary>
    public abstract class BusinessLogicManagerBase
    {
        public BusinessLogicManagerBase(
            ICallContext callContext
            )
        {
            this.CallContext = callContext;
        }

        #region Services

        protected ICallContext CallContext { get; private set; }

        #endregion
    }

}