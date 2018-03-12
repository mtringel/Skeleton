using System;
using System.Collections.Generic;
using TopTal.JoggingApp.Service.Models.Helpers;

namespace TopTal.JoggingApp.Service.Models.Shared
{
    /// <summary>
    /// Global variables returned to Angular, instead of rendering these values into global JavaScript variables by _GlobalVariablesPartial.cshtml
    /// (Angular does not support DOM or any other implementation specific details, global variables like document and window)
    /// DO NOT not expose entities from the BusinessEntities project to the clients!
    /// </summary>
    public class GlobalParameters : ServiceResult
    {
        public bool IsDebugging { get; set; }

        public Dictionary<string, string> Resources { get; set; }

        public string ProductTitle { get; set; }

        public int GridPageSize { get; set; }

        public string AutoLoginEmail { get; set; }

        public string AutoLoginPassword { get; set; }

        public string ApplicationPath { get; set; }

        public string RootPath { get; set; }
    }
}
