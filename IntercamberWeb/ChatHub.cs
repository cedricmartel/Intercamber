using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Business.Model;
using CML.Intercamber.Web.Helpers;
using Microsoft.AspNet.SignalR;

namespace CML.Intercamber.Web
{
    public class ChatHub : Hub
    {
        #region chat
        public const string RoomNameTemplate = "r{0}"; // threadId

        private bool CanEnterRoom(string idRoom)
        {
            return SessionHelper.ThreadDetails.Any(x => idRoom == string.Format(RoomNameTemplate, x.IdThread));
        }

        public void SendMessage(string idThread, string name, string message)
        {
            // verify rights on room 
            string targetRoom = string.Format(RoomNameTemplate, idThread);
            if (!CanEnterRoom(targetRoom))
                return;
            // save message 
            ThreadMessagesDao dao = new ThreadMessagesDao();
            dao.InsertThreadMessages(new ThreadMessages
            {
                DateMessage = DateTime.Now, 
                IdThread = long.Parse(idThread), 
                IdUser = SessionHelper.ConnectedUserId, 
                Message = message, 
                MessageCorrection = null
            });
            // send message 
            Clients.Group(targetRoom).addMessage(name, message, DateTimeHelper.FormatDate(DateTime.Now, DateTimeHelper.DATETIME_FORMAT));
        }

        public void AddToRoom(string idThread)
        {
            string targetRoom = string.Format(RoomNameTemplate, idThread);
            if (!CanEnterRoom(targetRoom))
                return;
            Groups.Add(Context.ConnectionId, targetRoom);
        }

        public void RemoveFromRoom(string roomName)
        {
            Groups.Remove(Context.ConnectionId, roomName);
        }
        #endregion 

        #region connecter users counter
        private static readonly List<long> listUserConnected = new List<long>();

        public static void RegisterUserConnected(long idUser, string emailUser)
        {
            if(!listUserConnected.Contains(idUser))
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
            foreach (var contact in SessionHelper.ContactDetails(idUser, emailUser))
            {
                if (listUserConnected.Contains(contact.IdUser))
                    hubContext.Clients.Group("u" + contact.IdUser).refreshOnlineStatus(idUser, amIConnected);
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
            long idUser = SessionHelper.ConnectedUserId;

            Groups.Add(Context.ConnectionId, "u" + idUser);

            return base.OnConnected();
        }

    }
}