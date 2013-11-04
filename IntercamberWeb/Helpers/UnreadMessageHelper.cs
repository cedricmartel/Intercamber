using System.Collections.Generic;
using System.Linq;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class UnreadMessageHelper
    {

        // contacts
        public static IList<UnreadMessagesCounter> ListUnreadMessages()
        {
            if (ConnectedUserHelper.ConnectedUser == null)
                return null;
            var userMail = ConnectedUserHelper.ConnectedUser.Email;
            List<UnreadMessagesCounter> data = SessionHelper.GetSessionItem(SessionHelper.UnreadMessageCount, userMail) as List<UnreadMessagesCounter>;
            if (data == null)
            {
                var dao = new ThreadMessagesDao();
                data = dao.UnreadMessagesCount(ConnectedUserHelper.ConnectedUserId);
                SessionHelper.AddSessionItem(SessionHelper.UnreadMessageCount, data);
            }
            return data;
        }


        public static void IncrementUnreadMessagesCount(long idUserSender, long idUserDest)
        {
            var user = ConnectedUserHelper.FindConnectedUser(idUserDest);
            if (user == null) return;
            List<UnreadMessagesCounter> data = SessionHelper.GetSessionItem(SessionHelper.UnreadMessageCount, user.Email) as List<UnreadMessagesCounter>;
            if (data != null)
            {
                var userMessage = data.FirstOrDefault(x => x.IdUser == idUserSender);
                if (userMessage != null)
                    userMessage.NbMessages++;
                else
                    data.Add(new UnreadMessagesCounter { IdUser = idUserSender, NbMessages = 1 });
            }
        }

        public static void RefreshCache(long connectedIdUser)
        {
            Users userInfos = ConnectedUserHelper.FindConnectedUser(connectedIdUser);
            if (userInfos == null)
                return;
            var userCache = SessionHelper.StaticSession[userInfos.Email];
            if (userCache.ContainsKey(SessionHelper.UnreadMessageCount))
                userCache.Remove(SessionHelper.UnreadMessageCount);
        }
    }
}