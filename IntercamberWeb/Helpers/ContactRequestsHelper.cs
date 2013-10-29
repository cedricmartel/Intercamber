using System.Collections.Generic;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class ContactRequestsHelper
    {


        // contacts
        public static List<UsersDetail> ContactRequestDetails(long idUser)
        {
            List<UsersDetail> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyContactRequests) as List<UsersDetail>;
            if (data == null)
            {
                var dao = new ContactRequestsDao();
                data = dao.ListContactRequests(idUser);
                SessionHelper.AddSessionItem(SessionHelper.SessionKeyContactRequests, data);
            }
            return data;
        }

        /// <summary>
        /// clear contactrequests cache for specific connected user 
        /// </summary>
        /// <param name="connectedIdUser"></param>
        public static void RefreshCache(long connectedIdUser)
        {
            Users userInfos = ConnectedUserHelper.FindConnectedUser(connectedIdUser);
            if (userInfos == null)
                return;
            var userCache = SessionHelper.StaticSession[userInfos.Email];
            if (userCache.ContainsKey(SessionHelper.SessionKeyContactRequests))
                userCache.Remove(SessionHelper.SessionKeyContactRequests);
        }

    }
}