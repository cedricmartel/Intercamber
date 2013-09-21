using System.Web;
using CML.Intercamber.Business.Constants;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class SessionHelper
    {
        private const string SessionKeyUser = "User";

        public static bool IsAdmin
        {
            get
            {
                Users user = (Users)HttpContext.Current.Session[SessionKeyUser];
                return user != null && user.IdProfil != null && user.IdProfil == ProfilsConst.Admin.Id;
            }
        }

        public static bool IsManagerEstablishment
        {
            get
            {
                Users user = (Users)HttpContext.Current.Session[SessionKeyUser];
                return user != null && user.IdProfil != null && user.IdProfil == ProfilsConst.Managerestablishment.Id;
            }
        }

        public static int? IdEstablishment
        {
            get
            {
                Users user = (Users)HttpContext.Current.Session[SessionKeyUser];
                return user != null ? user.IdEstablishment : null;
            }
        }

        public static Users ConnectedUser
        {
            get
            {
                return (Users)HttpContext.Current.Session[SessionKeyUser];
            }
        }

        public static void CreateSession(Users user)
        {
            var currentSession = HttpContext.Current.Session;
            currentSession[SessionKeyUser] = user;
            // TODO 
        }

        public static void CreateSession(string loginUser)
        {
            UsersDao dao = new UsersDao();
            Users connectedUser = dao.ListUserByEmail(loginUser);
            if (connectedUser != null)
                CreateSession(connectedUser);
        }

    }
}