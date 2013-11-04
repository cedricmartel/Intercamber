using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;
using CML.Intercamber.Web.Helpers;
using CML.Intercamber.Web.Models;
using MvcJqGrid;

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
            ViewBag.MyId = ConnectedUserHelper.ConnectedUserId;
            var discussionThreadInfo = ContactsHelper.ContactDetails(ConnectedUserHelper.ConnectedUserId).FirstOrDefault(x => x.IdUser == id);
            if (discussionThreadInfo != null)
                ViewBag.DestName = discussionThreadInfo.Name;
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
            List<UsersDetail> cd = ViewBag.MyContacts;
            UsersDetail contact = cd.FirstOrDefault(x => x.IdUser == id && x.NumUnreadMessages > 0);
            if (contact != null)
            {
                ThreadUsersDao threadUsersDao = new ThreadUsersDao();
                threadUsersDao.UpdateThreadRead(ConnectedUserHelper.ConnectedUserId, t.IdThread, DateTime.Now);
                contact.NumUnreadMessages = 0;
                UnreadMessageHelper.RefreshCache(id);
            }
            return View();
        }

        public const int NbMessagesPerHistoryRequest = 20;

        /// <summary>
        /// return latest NbMessagesPerHistoryRequest messages, before message id messageIdMax
        /// </summary>
        /// <param name="id"></param>
        /// <param name="messageIdMax"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChatHistory(long? id, long? messageIdMax)
        {
            ThreadMessagesDao dao = new ThreadMessagesDao();
            if (id == null || ThreadHelper.ThreadDetails.All(x => x.IdThread != id.Value))
                return null;

            var threads = ThreadHelper.ThreadDetails.ToList().Where(x => x.IdThread == id).ToList();
            var idUserConnecte = ConnectedUserHelper.ConnectedUserId;

            var historique = dao.ListThreadMessagesByParameters(id.Value, messageIdMax, NbMessagesPerHistoryRequest);
            IList<ChatMessage> res = historique.Select(x => new ChatMessage
            {
                Id = x.IdMessage,
                Author = x.IdUser == idUserConnecte ? ConnectedUserHelper.ConnectedUser.FirstName : threads.Where(t => t.IdUser == x.IdUser).Select(t => t.FirstName).FirstOrDefault(),
                IdUser = x.IdUser,
                Message = x.Message,
                Correction = x.MessageCorrection,
                Date = x.DateMessage
            }).ToList();

            return Json(new { d = res, ps = NbMessagesPerHistoryRequest }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddTranslation(long idThread, long idMessage, string correction)
        {
            ThreadMessagesDao dao = new ThreadMessagesDao();
            dao.CorrectThreadMessage(idMessage, correction);

            return Json(new { r = true }, JsonRequestBehavior.AllowGet);
        }

        #region page my discussions

        public ActionResult MyDiscussions()
        {
            return View();
        }

        public JsonResult MyDiscussionsData(GridSettings gridSettings)
        {
            ThreadsDao dao = new ThreadsDao();
            List<ThreadReportDetail> data = dao.ThreadReport(ConnectedUserHelper.ConnectedUserId);
            var res = new GridData<ThreadReportDetail>
            {
                page = 1, // number of requested page
                records = data.Count, // total records from query 
                total = 1, // total pages of query 
                rows = data
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}
