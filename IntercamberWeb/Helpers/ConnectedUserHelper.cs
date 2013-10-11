using System.Collections.Generic;
using System.Linq;
using System.Web;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Constants;
using CML.Intercamber.Business.Dao;

namespace CML.Intercamber.Web.Helpers
{
    public class ConnectedUserHelper
    {

        public static bool IsAdmin
        {
            get
            {
                Users user = SessionHelper.GetSessionItem(SessionHelper.SessionKeyUser) as Users;
                return user != null && user.IdProfil != null && user.IdProfil == ProfilsConst.Admin.Id;
            }
        }

        public static bool IsManagerEstablishment
        {
            get
            {
                Users user = SessionHelper.GetSessionItem(SessionHelper.SessionKeyUser) as Users;
                return user != null && user.IdProfil != null && user.IdProfil == ProfilsConst.Managerestablishment.Id;
            }
        }

        public static int? IdEstablishment
        {
            get
            {
                Users user = SessionHelper.GetSessionItem(SessionHelper.SessionKeyUser) as Users;
                return user != null ? user.IdEstablishment : null;
            }
        }

        public static long ConnectedUserId
        {
            get
            {
                var user = ((Users)SessionHelper.GetSessionItem(SessionHelper.SessionKeyUser));
                return user != null ? user.IdUser : 0;
            }
        }


        public static Users ConnectedUser
        {
            get
            {
                return (Users)SessionHelper.GetSessionItem(SessionHelper.SessionKeyUser);
            }
        }

        public static List<string> connectedUserSpokenLanguages
        {
            get
            {
                var user = ConnectedUser;
                if (user == null || user.UsersSpokenLanguages == null)
                    return new List<string>();
                return user.UsersSpokenLanguages.Select(x => x.IdLanguage).ToList();
            }
        }

        public static List<string> connectedUserLearnLanguages
        {
            get
            {
                var user = ConnectedUser;
                if (user == null || user.UsersLearnLanguages == null)
                    return new List<string>();
                return user.UsersLearnLanguages.Select(x => x.IdLanguage).ToList();
            }
        }

        public static void CreateSession(Users user)
        {
            HttpContext.Current.Session[SessionHelper.SessionKeyUserId] = user.IdUser;
            HttpContext.Current.Session[SessionHelper.SessionKeyUserEmail] = user.Email;
            ChatHub.RegisterUserConnected(user.IdUser, user.Email);
            SessionHelper.AddSessionItem(SessionHelper.SessionKeyUser, user, user.Email);
            // TODO ajouter les autres caches de session 
        }

        public static void CreateSession(string loginUser)
        {
            UsersDao dao = new UsersDao();
            Users connectedUser = dao.FindUserByEmail(loginUser);
            if (connectedUser != null)
                CreateSession(connectedUser);
        }

    }
}