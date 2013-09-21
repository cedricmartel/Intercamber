using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Business.Model;
using IBatisNet.Common;

namespace CML.Intercamber.Business.Dao
{
    public class ThreadMessagesDao
    {

		public IList<ThreadMessages> SearchThreadMessagess()
		{
			return IBatisHelper.Instance().QueryForList<ThreadMessages>("CML.Intercamber.ThreadMessages.SearchThreadMessagess", null);
		}

		public void UpdateThreadMessages(ThreadMessages obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Update("CML.Intercamber.ThreadMessages.UpdateThreadMessages", obj);
                session.Complete();
            }
		}

		public void InsertThreadMessages(ThreadMessages obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.ThreadMessages.InsertThreadMessages", obj);
                session.Complete();
            }
		}
		
		public void DeleteThreadMessages(string code)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
				// TODO  
                //IBatisHelper.Instance().Delete("CML.Intercamber.ThreadMessages.DeteleThreadMessages", code);
                //session.Complete();
            }
		}
	}
}

