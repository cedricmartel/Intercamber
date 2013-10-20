using System.Collections.Generic;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class ContactRequestsHelper
    {


        // contacts
        public static IList<ContactRequestDetail> ContactRequestDetails(long idUser)
        {
            IList<ContactRequestDetail> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyContactRequests) as List<ContactRequestDetail>;
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