//using IntercamberWeb.Filters;
//using IntercamberWeb.Models;

using System.Web.Mvc;

namespace CML.Intercamber.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {

            return View();
        }

    }
}
