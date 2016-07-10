using System.Web.Mvc;

namespace wreq.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Index","Crops");
        }        
    }
}