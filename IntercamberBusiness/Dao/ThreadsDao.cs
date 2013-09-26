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

    }
}

