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

            return View();
        }

        public JsonResult SearchData(GridSettings gridSettings, string country, string language)
        {
            if (country == null || language == null)
                return Json(null);

            UsersDao dao = new UsersDao();
            return Json(dao.SearchUsers(gridSettings, ConnectedUserHelper.ConnectedUserId, country, language, ConnectedUserHelper.connectedUserSpokenLanguages), JsonRequestBehavior.AllowGet);
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
