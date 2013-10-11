using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;
using CML.Intercamber.Web.Helpers;
using CML.Intercamber.Web.Models;

namespace CML.Intercamber.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class TalkController : BaseController
    {

        public ActionResult Discussion(long id)
        {
            ViewBag.MyThreads = ThreadHelper.ThreadDetails;
            ViewBag.MyName = ConnectedUserHelper.ConnectedUser.FirstName;
            var discussionThreadInfo = ContactsHelper.ContactDetails(ConnectedUserHelper.ConnectedUserId).FirstOrDefault(x => x.IdUser == id);
            if (discussionThreadInfo != null)
                ViewBag.DestName = discussionThreadInfo.NameFormated;
            else
                return RedirectToAction("Index", "Home");

            ThreadsDao dao = new ThreadsDao();
            Threads t = dao.GetThreads(ConnectedUserHelper.ConnectedUserId, id);
            if (t == null)
            {
                t = dao.CreateThreads(ConnectedUserHelper.ConnectedUserId, id);
                var allThreads = ThreadHelper.ThreadDetails;
                if (allThreads.All(x => x.IdThread != t.IdThread))
                    ThreadHelper.ReloadThreadDetails();
            }
            ViewBag.IdThread = t.IdThread;
            ViewBag.IdContact = id;

            
            // mark discussion as read
            List<ContactDetail> cd = ViewBag.MyContacts;
            ContactDetail contact = cd.FirstOrDefault(x => x.IdUser == id && x.NumUnreadMessages > 0);
            if (contact != null)
            {
                ThreadUsersDao threadUsersDao = new ThreadUsersDao();
                threadUsersDao.UpdateThreadRead(ConnectedUserHelper.ConnectedUserId, t.IdThread, DateTime.Now);
                contact.NumUnreadMessages = 0;
            }
            return View();
        }

        [HttpPost]
        public JsonResult ChatHistory(long? id)
        {
            IList<ChatMessage> res;
            ThreadMessagesDao dao = new ThreadMessagesDao();
            if (id == null || ThreadHelper.ThreadDetails.All(x => x.IdThread != id.Value))
                return null;

            var threads = ThreadHelper.ThreadDetails.ToList().Where(x => x.IdThread == id).ToList();
            var idUserConnecte = ConnectedUserHelper.ConnectedUserId;

            var historique = dao.ListThreadMessagesByParameters(id.Value);
            res = historique.Select(x => new ChatMessage
            {
                Author = x.IdUser == idUserConnecte ? ConnectedUserHelper.ConnectedUser.FirstName : threads.Where(t => t.IdUser == x.IdUser).Select(t => t.FirstName).FirstOrDefault(), 
                Message = x.Message, 
                Date = x.DateMessage
            }).ToList();

            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}
