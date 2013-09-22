using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CML.Intercamber.Web.App_Start;
using CML.Intercamber.Web.Helpers;

namespace CML.Intercamber.Web
{

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // signalr: Register the default hubs route: ~/signalr/hubs
            RouteTable.Routes.MapHubs();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // load connection string 
            Business.Helper.IBatisHelper.RegisterConnectionString(ConfigurationManager.ConnectionStrings["IntercamberDatabase"].ConnectionString);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 30; // 30 minutes timeout

            if (HttpContext.Current.IsDebuggingEnabled)
            {
                //-- Si authentifié, recharge l'information de l'utilisateur de la session.
                //-- Et le redirige sur sa page d'accueil
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string loginUser = HttpContext.Current.User.Identity.Name;
                    if (!string.IsNullOrEmpty(loginUser))
                    { 
                        SessionHelper.CreateSession(loginUser);
                    }
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            ChatHub.UsersConnectedCounter--;
        }
    }
}