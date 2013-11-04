using System.Linq;
using System.Web.Mvc;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;
using CML.Intercamber.Web.Helpers;
using MvcJqGrid;

namespace CML.Intercamber.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class ProfileController : BaseController
    {
        public ActionResult MyProfile()
        {
            var allCountries = CountryHelper.AllCountries;
            ViewBag.ContactCountries = allCountries;

            return View("~/Views/Profile/MyProfile.cshtml", ConnectedUserHelper.ConnectedUser);
        }


        [HttpPost]
        public ActionResult Edit(Users user)
        {
            user.IdUser = ConnectedUserHelper.ConnectedUserId;

            var dao = new UsersDao();
            Users updatedUser = dao.UpdateUser(user);
            if (updatedUser != null)
            {
                var updatedWholeBean = dao.FindUserByEmail(updatedUser.Email);
                ConnectedUserHelper.CreateSession(updatedWholeBean);
            }

            return MyProfile();
        }
    }

}
