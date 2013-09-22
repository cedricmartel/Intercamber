using System;
using System.Linq;
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
        private static int usersConnectedCounter = 0;

        public static int UsersConnectedCounter
        {
            get
            {
                return usersConnectedCounter;
            }
            set
            {
                usersConnectedCounter = value;
                string message = string.Format(Resources.Intercamber.NumberOnlineUsers, usersConnectedCounter);
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                hubContext.Clients.All.printOnlineUsers(message);
            }
        }

        public void GetCurrentOnlineUsers()
        {
            string message = string.Format(Resources.Intercamber.NumberOnlineUsers, usersConnectedCounter);
            Clients.Caller.printOnlineUsers(message);
        }
        #endregion
    }
}