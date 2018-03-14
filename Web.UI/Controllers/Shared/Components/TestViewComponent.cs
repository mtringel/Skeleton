using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.Web.UI.Controllers.Shared.Components
{
    [ViewComponent]
    public class TestViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string alma)
        {
            return View();
        }
    }
}
