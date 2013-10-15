using System.Resources;

namespace CML.Intercamber.Web.Helpers
{
    public class ResourcesHelper
    {
        private static ResourceManager Rm;

        public static string GetString(string resourceCode)
        {
            if(Rm == null)
                Rm = new ResourceManager("Resources.Intercamber", System.Reflection.Assembly.Load("App_GlobalResources"));
            return Rm.GetString(resourceCode);
        }
    }
}