using System.Collections.Generic;

namespace CML.Intercamber.Business.Dao
{
    public class ThreadUsersDao
    {

        //public IList<ThreadUsers> SearchThreadUserss()
        //{
        //    List<ThreadUsers> res;
        //    using (var context = new IntercamberEntities())
        //    {
        //        res = (from t in context.Threads
        //               where t.IdThread == idThread
        //               from tu in t.ThreadUsers
        //               join u in context.Users on tu.IdUser equals u.IdUser
        //               where u.Enabled
        //               select new ThreadDetail { FirstName = u.FirstName, LastName = u.LastName, IdThread = t.IdThread, IdUser = u.IdUser }
        //            ).ToList();
        //    }
        //    return res;
        //}

	}
}

