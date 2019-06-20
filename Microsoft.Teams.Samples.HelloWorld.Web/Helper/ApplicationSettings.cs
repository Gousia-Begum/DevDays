using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Helper
{
    public class ApplicationSettings
    {
        public static string AppName { get; set; } = "EventManagement";
        public static string BaseUrl { get; set; }
        public static string ConnectionName { get; set; }
        public static string AppId { get; set; }
        public static string AppSecret { get; set; }
        static ApplicationSettings()
        {
            ConnectionName = ConfigurationManager.AppSettings["ConnectionName"];
            BaseUrl = ConfigurationManager.AppSettings["BaseUri"];
            AppId = ConfigurationManager.AppSettings["MicrosoftAppId"];
            AppSecret = ConfigurationManager.AppSettings["MicrosoftAppPassword"];
        }
    }
}