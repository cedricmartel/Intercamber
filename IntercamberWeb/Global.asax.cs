using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Web.App_Start;
using CML.Intercamber.Web.Helpers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json;

namespace CML.Intercamber.Web
{

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Initialize log4net.
            log4net.Config.XmlConfigurator.Configure();
            LogHelper.LogInfo("Application Start");

            // signalr: Register the default hubs route: ~/signalr/hubs
            RouteTable.Routes.MapHubs();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // init json serializer cf http://stackoverflow.com/questions/13365294/signalr-datetime-format
            var serializer = new JsonNetSerializer(new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            });
            GlobalHost.DependencyResolver.Register(typeof(IJsonSerializer), () => serializer);

            // init rhino profiler
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Application Stop");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 30; // 30 minutes timeout

            if (HttpContext.Current.IsDebuggingEnabled)
            {
                // Si authentifié, recharge l'information de l'utilisateur de la session, et le redirige sur sa page d'accueil
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string loginUser = HttpContext.Current.User.Identity.Name;
                    if (!string.IsNullOrEmpty(loginUser))
                    {
                        ConnectedUserHelper.CreateSession(loginUser);
                    }
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            var idUser = Session[SessionHelper.SessionKeyUserId];
            var mailUser = Session[SessionHelper.SessionKeyUserEmail];
            if (idUser != null && mailUser != null)
            {
                ChatHub.RegisterUserDisconnected((long) idUser, (string) mailUser);
                SessionHelper.DestroySession((string) Session[SessionHelper.SessionKeyUserEmail]);
            }
        }

        /// <summary>
        /// log des erreurs via log4net
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                while (ex != null)
                {
                    ex.LogFatal();
                    ex = ex.InnerException;
                }
            }
        }
    }
}