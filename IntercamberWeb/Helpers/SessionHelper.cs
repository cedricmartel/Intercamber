using System.Collections.Generic;
using System.Linq;
using System.Web;
using CML.Intercamber.Business.Constants;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class SessionHelper
    {
        private const string SessionKeyUser = "User";
        private const string SessionKeyThreads = "Threads";

        // object for stocking sessions data (.net sessions object is not viable)
        private static readonly Dictionary<string, Dictionary<string, object>> Session = new Dictionary<string, Dictionary<string, object>>();

        #region

        public static object GetSessionItem(string code)
        {
            string idUser = HttpContext.Current.User.Identity.Name;
            if (!Session.Keys.Contains(idUser))
                return null;
            var connectedUserSession = Session[idUser];
            if (!connectedUserSession.ContainsKey(code))
                return null;
            return connectedUserSession[code];
        }

        public static void AddSessionItem(string code, object val)
        {
            AddSessionItem(code, val, HttpContext.Current.User.Identity.Name);
        }


        public static void AddSessionItem(string code, object val, string userKey)
        {
            if (!Session.Keys.Contains(userKey))
                Session.Add(userKey, new Dictionary<string, object>());
            var connectedUserSession = Session[userKey];
            if (!connectedUserSession.ContainsKey(code))
                connectedUserSession.Add(code, val);
            else
                connectedUserSession[code] = val;
        }


        #endregion

        #region data connected user
        public static bool IsAdmin
        {
            get
            {
                Users user = GetSessionItem(SessionKeyUser) as Users;
                return user != null && user.IdProfil != null && user.IdProfil == ProfilsConst.Admin.Id;
            }
        }

        public static bool IsManagerEstablishment
        {
            get
            {
                Users user = GetSessionItem(SessionKeyUser) as Users;
                return user != null && user.IdProfil != null && user.IdProfil == ProfilsConst.Managerestablishment.Id;
            }
        }

        public static int? IdEstablishment
        {
            get
            {
                Users user = GetSessionItem(SessionKeyUser) as Users;
                return user != null ? user.IdEstablishment : null;
            }
        }

        public static long ConnectedUserId
        {
            get {
                return ((Users)GetSessionItem(SessionKeyUser)).IdUser;
            }
        }


        public static Users ConnectedUser
        {
            get
            {
                return (Users)GetSessionItem(SessionKeyUser);
            }
        }

        public static void CreateSession(Users user)
        {
            ChatHub.UsersConnectedCounter++;
            AddSessionItem(SessionKeyUser, user, user.Email);
            // TODO 
        }

        public static void CreateSession(string loginUser)
        {
            UsersDao dao = new UsersDao();
            Users connectedUser = dao.ListUserByEmail(loginUser);
            if (connectedUser != null)
                CreateSession(connectedUser);
        }
        #endregion

        #region data threads 

        public static IList<ThreadDetail> ThreadDetails
        {
            get
            {
                IList<ThreadDetail> data = GetSessionItem(SessionKeyThreads) as List<ThreadDetail>;
                if(data == null)
                {
                    var dao = new ThreadsDao();
                    data = dao.ListThreads(ConnectedUserId);
                    AddSessionItem(SessionKeyThreads, data);
                }
                return data;
            }
        }
        #endregion

    }
}