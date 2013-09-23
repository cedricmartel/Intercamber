//using IntercamberWeb.Filters;
//using IntercamberWeb.Models;

using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;
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
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // connection failed
                ViewBag.ErrorMessage = "<span style='color:red;'>" + Resources.Intercamber.Login_BadLoginOrPassword + "</span>";
                return View(model);
            }
            
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
