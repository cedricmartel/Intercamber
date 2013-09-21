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
    public class ThreadUsersDao
    {

		public IList<ThreadUsers> SearchThreadUserss()
		{
			return IBatisHelper.Instance().QueryForList<ThreadUsers>("CML.Intercamber.ThreadUsers.SearchThreadUserss", null);
		}




		public void UpdateThreadUsers(ThreadUsers obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Update("CML.Intercamber.ThreadUsers.UpdateThreadUsers", obj);
                session.Complete();
            }
		}

		public void InsertThreadUsers(ThreadUsers obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.ThreadUsers.InsertThreadUsers", obj);
                session.Complete();
            }
		}
		
		public void DeleteThreadUsers(string code)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
				// TODO  
                //IBatisHelper.Instance().Delete("CML.Intercamber.ThreadUsers.DeteleThreadUsers", code);
                //session.Complete();
            }
		}
	}
}

