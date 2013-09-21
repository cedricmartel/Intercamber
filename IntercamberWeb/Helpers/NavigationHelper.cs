using System.Web;

namespace CML.Intercamber.Web.Helpers
{
    public static class NavigationHelper
    {
        public static string CurrentPageRoute
        {
            get
            {
                return "/" + HttpContext.Current.Request.RequestContext.RouteData.Values["controller"] + "/"
                    + HttpContext.Current.Request.RequestContext.RouteData.Values["action"];
            }
        }
    }
}