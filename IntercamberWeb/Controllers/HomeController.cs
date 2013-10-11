//using IntercamberWeb.Filters;
//using IntercamberWeb.Models;

using System.Web.Mvc;
using CML.Intercamber.Web.Helpers;

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
