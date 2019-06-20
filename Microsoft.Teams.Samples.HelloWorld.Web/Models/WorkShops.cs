using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Models
{
    public class WorkShops
    {
    }
    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Track { get; set; }
        public string WorkshopName { get; set; }
        //public string Introincludespeakerdescriptionanyequipmentenvironmentrequirements { get; set; }
        public string Intro { get; set; }
        public string Customquestionifneeded { get; set; }
        public string GEPwantstoconnect { get; set; }
        public string Certainpage { get; set; }
    }

}