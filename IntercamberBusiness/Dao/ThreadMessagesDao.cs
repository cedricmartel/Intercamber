using System;
using System.Collections.Generic;
using System.Linq;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Business.Dao
{
    public class ThreadMessagesDao
    {
        public List<ThreadMessages> ListThreadMessagesByParameters(long idThread)
        {
            List<ThreadMessages> res;
            using (var context = new IntercamberEntities())
            {
                res = context.ThreadMessages.Where(x => x.IdThread == idThread).ToList();
            }
            return res;
        }

        public void InsertThreadMessages(ThreadMessages obj)
        {
            using (var context = new IntercamberEntities())
            {
                context.ThreadMessages.Add(obj);
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

