namespace YTech.PruClient.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Bingo", "Contest");
        }

    }
}
