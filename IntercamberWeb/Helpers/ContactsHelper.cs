using System.Collections.Generic;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class ContactsHelper
    {

        // contacts
        public static IList<UsersDetail> ContactDetails(long idUser, string emailUser)
        {
            List<UsersDetail> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyContacts, emailUser) as List<UsersDetail>;
            if (data == null)
                return ContactDetails(idUser);
            return data;
        }
        public static List<UsersDetail> ContactDetails(long idUser)
        {
            List<UsersDetail> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyContacts) as List<UsersDetail>;
            if (data == null)
            {
                var dao = new ContactsDao();
                data = dao.ListContactsByUser(idUser);
                SessionHelper.AddSessionItem(SessionHelper.SessionKeyContacts, data);
            }
            return data;
        }

        public static void RefreshCache(long connectedIdUser)
        {
            Users userInfos = ConnectedUserHelper.FindConnectedUser(connectedIdUser);
            if (userInfos == null)
                return;
            var userCache = SessionHelper.StaticSession[userInfos.Email];
            if (userCache.ContainsKey(SessionHelper.SessionKeyContacts))
                userCache.Remove(SessionHelper.SessionKeyContacts);
        }
    }
}