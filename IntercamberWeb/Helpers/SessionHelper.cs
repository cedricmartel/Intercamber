using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CML.Intercamber.Web.Helpers
{
    public class SessionHelper
    {
        public const string SessionKeyUser = "User";
        public const string SessionKeyUserId = "IdUser";
        public const string SessionKeyUserEmail = "Email";
        public const string SessionKeyThreads = "Threads";
        public const string SessionKeyContacts = "Contacts";

        // object for stocking sessions data (.net sessions object is not viable)
        private static readonly Dictionary<string, Dictionary<string, object>> StaticSession = new Dictionary<string, Dictionary<string, object>>();


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

    }
}