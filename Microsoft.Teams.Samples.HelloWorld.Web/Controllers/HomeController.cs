using System.Web.Mvc;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("Index")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("hello")]
        public ActionResult Hello()
        {
            return View("Index");
        }

        [Route("first")]
        public ActionResult First()
        {
            return View("Index");
        }

        [Route("second")]
        public ActionResult Second()
        {
            return View("Index");
        }

        [Route("configure")]
        public ActionResult Configure()
        {
            return View();
        }
        [Route("firsttrack")]
        public ActionResult FirstTrack(string name)
        {
            return View(name);
        }
    }
}
