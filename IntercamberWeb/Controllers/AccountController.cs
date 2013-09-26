using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Web.Helpers;
using CML.Intercamber.Web.Models;

namespace CML.Intercamber.Web.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.HideLeftBar = true;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            UsersDao dao = new UsersDao();
            Users connectedUser = dao.Login(model.Email, model.Password);

            if (connectedUser != null)
            {
                // connection ok
                FormsAuthentication.SetAuthCookie(model.Email, false);
                SessionHelper.CreateSession(connectedUser);

                //success redirect 
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            // connection failed
            ViewBag.ErrorMessage = "<span style='color:red;'>" + Resources.Intercamber.Login_BadLoginOrPassword + "</span>";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public void ChangeLanguage(string id)
        {
            var cuka = new HttpCookie("_culture") {Expires = DateTime.Now.AddYears(10), Value=id};
            System.Web.HttpContext.Current.Response.Cookies.Add(cuka);
            Response.Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "/");
        }

    }
}
