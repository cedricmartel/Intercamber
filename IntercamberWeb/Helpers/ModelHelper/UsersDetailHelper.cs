using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers.ModelHelper
{
    public static class UsersDetailHelper
    {
        public static void FilDataInBean(UsersDetail u)
        {
            u.Connected = ChatHub.ListUserConnected.Contains(u.IdUser);
            if (string.IsNullOrEmpty(u.City))
                u.Location = ResourcesHelper.GetString("Countries_" + u.IdCountry);
            else
                u.Location = u.City + " (" + ResourcesHelper.GetString("Countries_" + u.IdCountry) + ")";
        }
    }
}