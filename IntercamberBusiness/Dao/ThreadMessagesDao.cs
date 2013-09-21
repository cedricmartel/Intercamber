using System.Collections.Generic;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Business.Model;
using IBatisNet.Common;

namespace CML.Intercamber.Business.Dao
{
    public class ThreadMessagesDao
    {
        public IList<ThreadMessages> ListThreadMessagesByParameters(long idThread)
		{
            Dictionary<string, object> p = new Dictionary<string, object> {{"IdThread", idThread}};
            return IBatisHelper.Instance().QueryForList<ThreadMessages>("CML.Intercamber.ThreadMessages.ListThreadMessagesByParameters", p);
		}

        public void InsertThreadMessages(ThreadMessages obj)
        {
            using (IDalSession session = IBatisHelper.Instance().BeginTransaction())
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.ThreadMessages.InsertThreadMessages", obj);
                session.Complete();
            }
        }



        //public void UpdateThreadMessages(ThreadMessages obj)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
        //    {
        //        IBatisHelper.Instance().Update("CML.Intercamber.ThreadMessages.UpdateThreadMessages", obj);
        //        session.Complete();
        //    }
        //}
		
        //public void DeleteThreadMessages(string code)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
        //    {
        //        // TODO  
        //        //IBatisHelper.Instance().Delete("CML.Intercamber.ThreadMessages.DeteleThreadMessages", code);
        //        //session.Complete();
        //    }
        //}
	}
}

