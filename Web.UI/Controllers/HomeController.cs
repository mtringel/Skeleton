using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopTal.JoggingApp.Azure;
using TopTal.JoggingApp.Security.Managers;
using TopTal.JoggingApp.Web.UI.Models;

namespace TopTal.JoggingApp.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : Helpers.ControllerBase
    {
        public HomeController(
            CallContext.ICallContext callContext,
            Security.Managers.IAuthProvider authProvider,
            Configuration.AppConfig appConfig
            )
            : base(callContext, authProvider, appConfig)
        {
        }

        public IActionResult Index()
        {
            return View();            
        }

        public IActionResult About()
        {
            // Test Graph query
            ViewData["Message"] = "Your application description page.";

            try
            {
                var graph = new GraphClient(AppConfig, "3a9d8c99-f7d8-4418-a7de-1f864008974a");
                var result = graph.GetAllUsers(null).Result;
                var objId = this.AuthProvider.CurrentUser.ObjectId;
                //var result = B2CGraphClient.GetUserByObjectId(objId).Result;
                ViewData["Response"] = result;
            }
            catch (Exception ex)
            {
                ViewData["Response"] = ex.ToString();
            }

            return View();
        }

        public IActionResult Contact()
        {
            // Test Application Insights failed request
            throw new Exception("Test exception");

            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
