//using IntercamberWeb.Filters;
//using IntercamberWeb.Models;

using System.Web.Mvc;

namespace CML.Intercamber.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class TalkController : BaseController
    {

        public ActionResult Discussion()
        {
            
            return View();
        }

    }
}
