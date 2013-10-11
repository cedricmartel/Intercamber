using System.Web.Mvc;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Web.Helpers;
using MvcJqGrid;

namespace CML.Intercamber.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class ContactController : BaseController
    {

        public ActionResult Search()
        {   
            return View();
        }

        public JsonResult SearchData(GridSettings gridSettings)
        {
            UsersDao dao = new UsersDao();
            return Json(dao.SearchUsers(gridSettings, ConnectedUserHelper.ConnectedUserId, ConnectedUserHelper.connectedUserSpokenLanguages, ConnectedUserHelper.connectedUserLearnLanguages), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AskContact(long idUser, string message)
        {
            ContactRequestsDao dao = new ContactRequestsDao();
            return Json(dao.InsertContactRequest(new ContactRequests
            {
                IdUserAsk = ConnectedUserHelper.ConnectedUserId, 
                IdUserRequester = idUser, 
                Message = message
            }));
        }
    


    }

}
