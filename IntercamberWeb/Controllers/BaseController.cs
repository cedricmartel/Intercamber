using System;
using System.Linq;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Web.Helpers;
using WebGrease.Css.Extensions;

namespace CML.Intercamber.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback c, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];

            // If there is a cookie already with the language, use the value for the translation, else uses the default language configured.
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
            {
                cultureName = ConfigurationManager.AppSettings["DefaultCultureName"];

                cultureCookie = new HttpCookie("_culture");
                cultureCookie.HttpOnly = false; // Not accessible by JS.
                cultureCookie.Expires = DateTime.Now.AddYears(1);
            }

            // Validates the culture name.
            //cultureName = CultureHelper.GetImplementedCulture(cultureName);

            // Sets the new language to the cookie.
            cultureCookie.Value = cultureName;

            // Sets the cookie on the response.
            Response.Cookies.Add(cultureCookie);

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            // load chat contacts
            if (Request.IsAuthenticated)
            {
                var contactInfos = ContactsHelper.ContactDetails(ConnectedUserHelper.ConnectedUserId);
                
                // TODO gérer un cache pour les messages non lus
                ThreadMessagesDao messagesDao = new ThreadMessagesDao();
                var unreadMessagesInfos = messagesDao.UnreadMessagesCount(ConnectedUserHelper.ConnectedUserId);
                contactInfos.ForEach(x =>
                {
                    x.NumUnreadMessages = unreadMessagesInfos.Where(y => y.IdUser == x.IdUser).Select(y => y.NbMessages).FirstOrDefault();
                });
                ViewBag.MyContacts = contactInfos;
                ViewBag.ConnectedUserInfo = ConnectedUserHelper.ConnectedUser;
                ViewBag.NumContactRequests = ContactRequestsHelper.ContactRequestDetails(ConnectedUserHelper.ConnectedUserId).Count;
            }
            ViewBag.IsAdmin = ConnectedUserHelper.IsAdmin;

            return base.BeginExecuteCore(c, state);
        }

    }
}
