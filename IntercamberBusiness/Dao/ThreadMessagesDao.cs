using System.Collections.Generic;
using System.Linq;

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

	}
}

