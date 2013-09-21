using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Business.Model;
using CML.Intercamber.Web.Helpers;
using CML.Intercamber.Web.Models;

namespace CML.Intercamber.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class TalkController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of thread</param>
        /// <returns></returns>
        public ActionResult Discussion(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // get thread infos
            var threads = SessionHelper.ThreadDetails;
            var currentThread = threads.Where(x => x.IdThread == id);
            ViewBag.CurrentThread = currentThread;
            ViewBag.MyName = SessionHelper.ConnectedUser.UserNameComplete;
            ViewBag.IdThread = id;
            return View();
        }

        [HttpPost]
        public JsonResult ChatHistory(long? id)
        {
            IList<ChatMessage> res = null;
            ThreadMessagesDao dao = new ThreadMessagesDao();
            if (id == null || SessionHelper.ThreadDetails.All(x => x.IdThread != id.Value))
                return null;

            var threads = SessionHelper.ThreadDetails.ToList().Where(x => x.IdThread == id).ToList();
            var idUserConnecte = SessionHelper.ConnectedUserId;

            var historique = dao.ListThreadMessagesByParameters(id.Value);
            res = historique.Select(x => new ChatMessage()
            {
                Author = x.IdUser == idUserConnecte ? SessionHelper.ConnectedUser.FirstName : threads.Where(t => t.IdUser == x.IdUser).Select(t => t.FirstName).FirstOrDefault(), 
                Message = x.Message, 
                Date = DateTimeHelper.FormatDate(x.DateMessage, DateTimeHelper.DATETIME_FORMAT)
            }).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Threads()
        {
            ViewBag.MyThreads = SessionHelper.ThreadDetails;
            return View();
        }

    }
}
