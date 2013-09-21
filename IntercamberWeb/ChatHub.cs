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
            dao.InsertThreadMessages(new ThreadMessages()
            {
                DateMessage = DateTime.Now, 
                IdThread = long.Parse(idThread), 
                IdUser = SessionHelper.ConnectedUserId, 
                Message = message, 
                MessageCorrection = null
            });
            // send message 
            Clients.Group(targetRoom).add(name, message, DateTimeHelper.FormatDate(DateTime.Now, DateTimeHelper.DATETIME_FORMAT));
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
    }
}