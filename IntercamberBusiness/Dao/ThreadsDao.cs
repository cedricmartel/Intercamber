using System.Collections.Generic;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Business.Dao
{
    public class ThreadsDao
    {

        public IList<ThreadDetail> ListThreads(long idUser)
        {
            return IBatisHelper.Instance().QueryForList<ThreadDetail>("CML.Intercamber.Threads.UserThreads", idUser);
        }

        public IList<ThreadDetail> ThreadDetail(long idThread)
        {
            return IBatisHelper.Instance().QueryForList<ThreadDetail>("CML.Intercamber.Threads.ThreadDetailsById", idThread);
        }
        

        // TODO remove bellow 

        //public IList<Threads> SearchThreadss()
        //{
        //    return IBatisHelper.Instance().QueryForList<Threads>("CML.Intercamber.Threads.SearchThreadss", null);
        //}

        //public void UpdateThreads(Threads obj)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
        //    {
        //        IBatisHelper.Instance().Update("CML.Intercamber.Threads.UpdateThreads", obj);
        //        session.Complete();
        //    }
        //}

        //public void InsertThreads(Threads obj)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
        //    {
        //        IBatisHelper.Instance().Insert("CML.Intercamber.Threads.InsertThreads", obj);
        //        session.Complete();
        //    }
        //}
		
        //public void DeleteThreads(string code)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
        //    {
        //        // TODO  
        //        //IBatisHelper.Instance().Delete("CML.Intercamber.Threads.DeteleThreads", code);
        //        //session.Complete();
        //    }
        //}
	}
}

