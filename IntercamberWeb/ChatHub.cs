using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Web.Helpers;
using Microsoft.AspNet.SignalR;

namespace CML.Intercamber.Web
{
    public class ChatHub : Hub
    {
        #region chat

        private bool CanTalkInThread(long idThread)
        {
            return ThreadHelper.ThreadDetails.Any(x => idThread == x.IdThread);
        }

        /// <summary>
        /// send a message from client side 
        /// </summary>
        /// <param name="idContact">dest</param>
        /// <param name="idThread"></param>
        /// <param name="name">sender display name</param>
        /// <param name="message"></param>
        /// <returns>new message id</returns>
        public long? SendMessage(long idContact, long idThread, string name, string message)
        {
            // verify rights on room 
            if (!CanTalkInThread(idThread))
                return null;
            // save message 
            ThreadMessagesDao dao = new ThreadMessagesDao();
            long idMessage = dao.InsertThreadMessages(new ThreadMessages
            {
                DateMessage = DateTime.Now,
                IdThread = idThread,
                IdUser = ConnectedUserHelper.ConnectedUserId,
                Message = message,
                MessageCorrection = null
            });
            // send message to anybody in thread but sender
            foreach (var thread in ThreadHelper.ThreadDetails.Where(x => x.IdThread == idThread))
                Clients.Group(thread.IdUser.ToString()).addMessage(idMessage, ConnectedUserHelper.ConnectedUserId, idThread, name, ConnectedUserHelper.ConnectedUserId, message, DateTime.Now);
            return idMessage;
        }

        #endregion

        #region connecter users counter
        public static readonly List<long> listUserConnected = new List<long>();

        public static List<long> ListUserConnected { get { return listUserConnected; } }

        public static void RegisterUserConnected(long idUser, string emailUser)
        {
            if (!listUserConnected.Contains(idUser))
                listUserConnected.Add(idUser);
            PublishUserCountAndFriendsOnlineStatus(idUser, emailUser);
        }

        public static void RegisterUserDisconnected(long idUser, string emailUser)
        {
            if (listUserConnected.Contains(idUser))
                listUserConnected.Remove(idUser);
            PublishUserCountAndFriendsOnlineStatus(idUser, emailUser);
        }

        public static bool IsUserConnected(long idUser)
        {
            return listUserConnected.Contains(idUser);
        }

        private static void PublishUserCountAndFriendsOnlineStatus(long idUser, string emailUser)
        {
            // change number connected users
            string message = string.Format(Resources.Intercamber.NumberOnlineUsers, listUserConnected.Count);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.printOnlineUsers(message);
            // change users online status  
            bool amIConnected = listUserConnected.Contains(idUser);
            foreach (var contact in ContactsHelper.ContactDetails(idUser, emailUser))
            {
                if (listUserConnected.Contains(contact.IdUser))
                    hubContext.Clients.Group(contact.IdUser.ToString()).refreshOnlineStatus(idUser, amIConnected);
            }
        }

        public void GetCurrentOnlineUsers()
        {
            string message = string.Format(Resources.Intercamber.NumberOnlineUsers, listUserConnected.Count);
            Clients.Caller.printOnlineUsers(message);
        }
        #endregion


        public override Task OnConnected()
        {
            // add user to group that has same id as him
            long idUser = ConnectedUserHelper.ConnectedUserId;
            Groups.Add(Context.ConnectionId, idUser.ToString());
            return base.OnConnected();
        }

    }
}