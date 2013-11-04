using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;
using CML.Intercamber.Web.Helpers;
using CML.Intercamber.Web.Helpers.ModelHelper;
using MvcJqGrid;

namespace CML.Intercamber.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class ContactController : BaseController
    {
        #region page search contacts
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
            GridData<UsersDetail> res = dao.SearchUsers(gridSettings, ConnectedUserHelper.ConnectedUserId, country, language, ConnectedUserHelper.connectedUserSpokenLanguages);
            res.rows.ForEach(UsersDetailHelper.FilDataInBean);

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
        #endregion

        #region page my penpals
        public ActionResult MyPenpals()
        {
            return View();
        }
        
        public JsonResult PendingRequestsData(GridSettings gridSettings)
        {
            var myContactsRequests = ContactRequestsHelper.ContactRequestDetails(ConnectedUserHelper.ConnectedUserId);
            myContactsRequests.ForEach(UsersDetailHelper.FilDataInBean);
            var res = new GridData<UsersDetail>
            {
                page = 1, // number of requested page
                records = myContactsRequests.Count, // total records from query 
                total = 1, // total pages of query 
                rows = myContactsRequests
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidateRequest(long idUser)
        {
            var reqDao = new ContactRequestsDao();
            bool resInsert = reqDao.ValidateRefuseContactRequest(idUser, ConnectedUserHelper.ConnectedUserId, true);
            ContactsHelper.RefreshCache(idUser);
            ContactsHelper.RefreshCache(ConnectedUserHelper.ConnectedUserId);
            ContactRequestsHelper.RefreshCache(ConnectedUserHelper.ConnectedUserId);

            return Json(resInsert);
        }

        [HttpPost]
        public JsonResult RefuseRequest(long idUser)
        {
            var reqDao = new ContactRequestsDao();
            bool resRefuse = reqDao.ValidateRefuseContactRequest(idUser, ConnectedUserHelper.ConnectedUserId, false);
            ContactRequestsHelper.RefreshCache(ConnectedUserHelper.ConnectedUserId);
            return Json(resRefuse);
        }

        public JsonResult MyPenpalsData(GridSettings gridSettings)
        {
            var myPenpals = ContactsHelper.ContactDetails(ConnectedUserHelper.ConnectedUserId);
            myPenpals.ForEach(UsersDetailHelper.FilDataInBean);
            var res = new GridData<UsersDetail>
            {
                page = 1, // number of requested page
                records = myPenpals.Count, // total records from query 
                total = 1, // total pages of query 
                rows = myPenpals
            };


            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion 
    }

}
