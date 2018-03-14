using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopTal.JoggingApp.AzureHelper.Graph;
using TopTal.JoggingApp.Web.UI.Controllers.Shared.Components;
using TopTal.JoggingApp.Web.UI.Models;

namespace TopTal.JoggingApp.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : Helpers.ControllerBase
    {
        public HomeController(
            CallContext.ICallContext callContext,
            Security.Managers.IAuthProvider authProvider,
            Configuration.AppConfig appConfig,
            Microsoft.Extensions.Configuration.IConfiguration configuration
            )
            : base(callContext, authProvider, appConfig)
        {
            this.Configuration = configuration;
        }

        private Microsoft.Extensions.Configuration.IConfiguration Configuration;

        public IActionResult Index()
        {
            return View();            
        }

        public IActionResult About()
        {           
            return View();
        }

        public IActionResult Contact()
        {                        
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Debug()
        {
            #region Test Configuration Settings

            ViewBag.Settings1 = $"Configuration[\"AppDb\"]={Configuration["AppDb"]}";
            ViewBag.Settings2 = $"Configuration[\"ConnectionStrings.AppDb\"]={Configuration["ConnectionStrings.AppDb"]}";
            ViewBag.Settings3 = $"Configuration.GetSection(\"ConnectionStrings\")[\"AppDb\"]={Configuration.GetSection("ConnectionStrings")["AppDb"]}";
            ViewBag.Settings4 = $"Configuration[\"TestSetting\"]={Configuration["TestSetting"]}";
            ViewBag.Settings5 = $"Configuration.GetSection(\"ServiceApi\")[\"MaximumReturnedRows\"]={Configuration.GetSection("ServiceApi")["MaximumReturnedRows"]}";
            ViewBag.Settings6 = $"Configuration[\"ServiceApi.MaximumReturnedRows\"]={Configuration["ServiceApi.MaximumReturnedRows"]}";
            ViewBag.Settings7 = $"AppConfig.ServiceApi.MaximumReturnedRows={AppConfig.ServiceApi.MaximumReturnedRows}";
            ViewBag.Settings8 = $"AppConfig.ConnectionStrings.AppDb={AppConfig.ConnectionStrings.AppDb}";

            #endregion

            #region Test Exception Logging

            // Test Application Insights failed request
            //throw new Exception("Test exception");

            #endregion

            #region Test Graph API

            //// Test Graph query

            try
            {
                var graphClient = new GraphClient(AppConfig, "3a9d8c99-f7d8-4418-a7de-1f864008974a");
                //    var result = graphClient.GetAllUsers(null).Result;
                //    var objId = this.AuthProvider.CurrentUser.ObjectId;
                //    //var result = B2CGraphClient.GetUserByObjectId(objId).Result;
                //    ViewBag.Response = result;

                ViewBag.Response = graphClient.GraphTest().Result;
            }
            catch (Exception ex)
            {
                ViewBag.Response = ex.ToString();
            }

            #endregion

            return View(); 
        }

        public new IActionResult ViewComponent(string alma)
        {
            var res = ViewComponent(typeof(TestViewComponent), new { alma });
            return res;

        }
    }
}
