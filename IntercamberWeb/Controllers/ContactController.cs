using System.Linq;
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
            var allLanguages = LanguageHelper.AllLanguages;
            var allCountries = CountryHelper.AllCountries;
            ViewBag.ContactCountries = allCountries;
            ViewBag.ContactLanguages = allLanguages.Where(x => ConnectedUserHelper.connectedUserLearnLanguages.Contains(x.IdLanguage)).ToList();
            Languages firstLanguage = null;
            if (ConnectedUserHelper.connectedUserLearnLanguages != null && ConnectedUserHelper.connectedUserLearnLanguages.Count > 0)
                firstLanguage = allLanguages.FirstOrDefault(x => x.IdLanguage == ConnectedUserHelper.connectedUserLearnLanguages[0]);
            ViewBag.PreferedCountry = firstLanguage != null ? firstLanguage.PreferedCountry : "";
            ViewBag.ContactsTableDataUrl = "/Contact/SearchData";

            return View();
        }


        public ActionResult MyPenpals()
        {
            // TODO 

            return View();
        }

        public JsonResult SearchData(GridSettings gridSettings, string country, string language)
        {
            if (country == null || language == null)
                return Json(null);

            var connectedUsers = ChatHub.ListUserConnected;
            UsersDao dao = new UsersDao();
            var res = dao.SearchUsers(gridSettings, ConnectedUserHelper.ConnectedUserId, country, language, ConnectedUserHelper.connectedUserSpokenLanguages);

            // fill country & connected status
            foreach (var contact in res.rows)
            {
                var contactCountry = contact.IdCountry;
                if (!string.IsNullOrEmpty(contactCountry))
                    contact.City += " (" + ResourcesHelper.GetString("Countries_" + contactCountry) + ")";
                contact.Connected = connectedUsers.Contains(contact.IdUser);
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AskContact(long idUser, string message)
        {
            ContactRequestsDao dao = new ContactRequestsDao();

            bool resInsert = dao.InsertContactRequest(new ContactRequests
            {
                IdUserAsk = idUser,
                IdUserRequester = ConnectedUserHelper.ConnectedUserId,
                Message = message
            });

            if (resInsert)
                ContactRequestsHelper.RefreshCache(idUser);

            return Json(resInsert);
        }



    }

}
