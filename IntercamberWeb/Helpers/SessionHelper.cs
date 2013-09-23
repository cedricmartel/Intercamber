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
        public const string SessionKeyUserId = "IdUser";
        public const string SessionKeyUserEmail = "Email";
        private const string SessionKeyThreads = "Threads";
        private const string SessionKeyContacts = "Contacts";

        // object for stocking sessions data (.net sessions object is not viable)
        private static readonly Dictionary<string, Dictionary<string, object>> StaticSession = new Dictionary<string, Dictionary<string, object>>();

        #region

        public static object GetSessionItem(string code)
        {
            return GetSessionItem(code, HttpContext.Current.User.Identity.Name);
        }
            

        public static object GetSessionItem(string code, string idUser)
        {
            if (!StaticSession.Keys.Contains(idUser))
                return null;
            var connectedUserSession = StaticSession[idUser];
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
            if (!StaticSession.Keys.Contains(userKey))
                StaticSession.Add(userKey, new Dictionary<string, object>());
            var connectedUserSession = StaticSession[userKey];
            if (!connectedUserSession.ContainsKey(code))
                connectedUserSession.Add(code, val);
            else
                connectedUserSession[code] = val;
        }

        public static void DestroySession(string userKey)
        {
            if (StaticSession.ContainsKey(userKey))
                StaticSession.Remove(userKey);
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
            get
            {
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
            HttpContext.Current.Session[SessionKeyUserId] = user.IdUser;
            HttpContext.Current.Session[SessionKeyUserEmail] = user.Email;
            ChatHub.RegisterUserConnected(user.IdUser, user.Email);
            AddSessionItem(SessionKeyUser, user, user.Email);
            // TODO ajouter les autres caches de session 
        }

        public static void CreateSession(string loginUser)
        {
            UsersDao dao = new UsersDao();
            Users connectedUser = dao.ListUserByEmail(loginUser);
            if (connectedUser != null)
                CreateSession(connectedUser);
        }
        #endregion

        // data threads 
        public static IList<ThreadDetail> ThreadDetails
        {
            get
            {
                IList<ThreadDetail> data = GetSessionItem(SessionKeyThreads) as List<ThreadDetail>;
                if (data == null)
                {
                    var dao = new ThreadsDao();
                    data = dao.ListThreads(ConnectedUserId);
                    AddSessionItem(SessionKeyThreads, data);
                }
                return data;
            }
        }

        // contacts
        public static IList<ContactDetail> ContactDetails(long idUser, string emailUser)
        {
            IList<ContactDetail> data = GetSessionItem(SessionKeyContacts, emailUser) as List<ContactDetail>;
            if (data == null)
            {
                var dao = new ContactsDao();
                data = dao.ListContactsByUser(idUser);
                AddSessionItem(SessionKeyContacts, data, emailUser);
            }
            return data;
        }

    }
}