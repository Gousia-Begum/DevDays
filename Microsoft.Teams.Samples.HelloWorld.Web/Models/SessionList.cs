using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Models
{
    public class SessionList
    {
        public string Session { get; set; }
        public string Owner { get; set; }
        public string Speaker { get; set; }
        public object Duration { get; set; }
        public string SessionType { get; set; }
        public string Track { get; set; }
        public string Abstract { get; set; }
        public string PRQuotes { get; set; }
    }

}