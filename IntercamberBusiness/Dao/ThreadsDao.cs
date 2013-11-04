using System;
using System.Collections.Generic;
using CML.Intercamber.Business.Model;
using System.Linq;

namespace CML.Intercamber.Business.Dao
{
    public class ThreadsDao
    {

        public List<ThreadDetail> ListThreads(long idUser)
        {
            List<ThreadDetail> res;
            using (var context = new IntercamberEntities())
            {
                res = (from t in context.Threads
                       where t.ThreadUsers.Any(ttu => ttu.IdUser == idUser)
                       from tu in t.ThreadUsers
                       where tu.IdUser != idUser
                       join u in context.Users on tu.IdUser equals u.IdUser
                       where u.Enabled
                       select new ThreadDetail { FirstName = u.FirstName, LastName = u.LastName, IdThread = t.IdThread, IdUser = u.IdUser }
                    ).ToList();
            }
            return res;
        }

        public List<ThreadDetail> ThreadDetail(long idThread)
        {
            List<ThreadDetail> res;
            using (var context = new IntercamberEntities())
            {
                res = (from t in context.Threads
                       where t.IdThread == idThread
                       from tu in t.ThreadUsers
                       join u in context.Users on tu.IdUser equals u.IdUser
                       where u.Enabled
                       select new ThreadDetail { FirstName = u.FirstName, LastName = u.LastName, IdThread = t.IdThread, IdUser = u.IdUser }
                    ).ToList();
            }
            return res;
        }

        public Threads GetThreads(long connectedUser, long destUser)
        {
            Threads res;
            using (var context = new IntercamberEntities())
            {
                res = context.Threads.FirstOrDefault(x =>
                    x.ThreadUsers.Any(y => y.IdThread == x.IdThread && y.IdUser == connectedUser) &&
                    x.ThreadUsers.Any(y => y.IdThread == x.IdThread && y.IdUser == destUser));
            }
            return res;
        }
        public Threads CreateThreads(long connectedUser, long destUser)
        {
            Threads res;
            using (var context = new IntercamberEntities())
            {
                res = new Threads {IdTypeThread = 1};
                res.ThreadUsers.Add(new ThreadUsers { IdUser = connectedUser } );
                res.ThreadUsers.Add(new ThreadUsers { IdUser = destUser });
                context.Threads.Add(res);
                context.SaveChanges();
            }
            return res;
        }


        public List<ThreadReportDetail> ThreadReport(long idUserConnected)
        {
            List<ThreadReportDetail> res;
            using (var context = new IntercamberEntities())
            {
                res = (
                    from tu in context.ThreadUsers
                    where tu.IdUser == idUserConnected
                    join t in context.Threads on tu.IdThread equals t.IdThread
                    join tm in context.ThreadMessages on t.IdThread equals tm.IdThread
                    join tuu in context.ThreadUsers on tu.IdThread equals tuu.IdThread
                    where tuu.IdUser != idUserConnected
                    join u in context.Users on tuu.IdUser equals u.IdUser
                    select new 
                    {
                        tu.IdThread, 
                        msgUser = tm.IdUser, 
                        msgDate = tm.DateMessage, 
                        tuu.IdUser, 
                        u.LastName, 
                        u.FirstName
                    } into x
                    group x by new { x.IdThread, x.IdUser, x.LastName, x.FirstName } into g
                    select new ThreadReportDetail
                    {
                        IdThread  = g.Key.IdThread, 
                        IdUser = g.Key.IdUser, 
                        NameUser = g.Key.FirstName + " " + g.Key.LastName, 
                        DateStart = g.Min(z => z.msgDate), 
                        DateLastMessageReceived = g.Max(z => z.msgUser == z.IdUser ? z.msgDate : DateTime.MinValue),
                        DateLastMessageSent = g.Max(z => z.msgUser == idUserConnected ? z.msgDate : DateTime.MinValue), 
                        NumberMessagesReceived = g.Count(z => z.msgUser == z.IdUser),
                        NumberMessagesSent = g.Count(z => z.msgUser != z.IdUser)
                    }).ToList();
            }
            return res;
        }
    }
}

