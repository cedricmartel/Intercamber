using System;
using System.Collections.Generic;
using System.Linq;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Business.Dao
{
    public class ThreadMessagesDao
    {
        public List<ThreadMessages> ListThreadMessagesByParameters(long idThread, long? messagesBefore, int nbMessages)
        {
            List<ThreadMessages> res;
            using (var context = new IntercamberEntities())
            {
                var req = context.ThreadMessages.Where(x => x.IdThread == idThread);
                if (messagesBefore != null)
                    req = req.Where(x => x.IdMessage < messagesBefore.Value);
                res = req.OrderByDescending(x => x.IdMessage).Take(nbMessages).ToList();
            }
            return res;
        }

        public long InsertThreadMessages(ThreadMessages obj)
        {
            using (var context = new IntercamberEntities())
            {
                context.ThreadMessages.Add(obj);
                context.SaveChanges();
            }
            return obj.IdMessage;
        }

        public void CorrectThreadMessage(long idMessage, string correctionString)
        {
            using (var context = new IntercamberEntities())
            {
                var message = context.ThreadMessages.Where(x => x.IdMessage == idMessage).FirstOrDefault();
                if (message == null)
                    return;
                message.MessageCorrection = correctionString;
                context.SaveChanges();
            }
        }

        public List<UnreadMessagesCounter> UnreadMessagesCount(long idUser)
        {
            List<UnreadMessagesCounter> res;
            using (var context = new IntercamberEntities())
            {
                var userThreads = (from tu in context.ThreadUsers
                                   where tu.IdUser == idUser
                                   select new { tu.IdThread, tu.DateLastSeen });
                res = (from t in context.Threads
                       join ut in userThreads on t.IdThread equals ut.IdThread
                       from m in t.ThreadMessages
                       where (ut.DateLastSeen == null || m.DateMessage > ut.DateLastSeen) && m.IdUser != idUser
                       group m by new { m.IdUser } into g
                       select new UnreadMessagesCounter() {IdUser = g.Key.IdUser,  NbMessages = g.Count()}).ToList();

            }
            return res;
        }

    }
}

