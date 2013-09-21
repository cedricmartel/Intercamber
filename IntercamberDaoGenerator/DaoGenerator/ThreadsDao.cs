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
    public class ThreadsDao
    {

		public IList<Threads> SearchThreadss()
		{
			return IBatisHelper.Instance().QueryForList<Threads>("CML.Intercamber.Threads.SearchThreadss", null);
		}

		public void UpdateThreads(Threads obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Update("CML.Intercamber.Threads.UpdateThreads", obj);
                session.Complete();
            }
		}

		public void InsertThreads(Threads obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.Threads.InsertThreads", obj);
                session.Complete();
            }
		}
		
		public void DeleteThreads(string code)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
				// TODO  
                //IBatisHelper.Instance().Delete("CML.Intercamber.Threads.DeteleThreads", code);
                //session.Complete();
            }
		}
	}
}

