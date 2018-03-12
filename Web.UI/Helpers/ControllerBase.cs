using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopTal.JoggingApp.CallContext;
using TopTal.JoggingApp.Configuration;
using Microsoft.Extensions.Configuration;
using TopTal.JoggingApp.BusinessLogic;
using TopTal.JoggingApp.CallContext.Web;
using TopTal.JoggingApp.Security.Managers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TopTal.JoggingApp.Web.UI.Helpers
{
    public abstract class ControllerBase : Controller
    {
        public ControllerBase(
            ICallContext callContext,
            IAuthProvider authProvider,
            AppConfig appConfig
            )
        {
            this.CallContext = callContext;
            this.AuthProvider = authProvider;
            this.AppConfig = appConfig;
        }

        #region Services

        public ICallContext CallContext { get; private set; }

        public IAuthProvider AuthProvider { get; private set; }

        public AppConfig AppConfig { get; private set; }

        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // don't do it in the constructor, that's too early
            ViewBag.ActiveController = this;
        }

        #region Globals

        public string ApplicationPath { get { return AppConfig.WebApplication.BasePath; } }

        public string RootPath { get { return AppConfig.WebApplication.BaseUrl; } }

#if DEBUG
        public readonly bool IsDebugging = true;
#else
        public readonly bool IsDebugging = false;
#endif

        #endregion
    }
}
