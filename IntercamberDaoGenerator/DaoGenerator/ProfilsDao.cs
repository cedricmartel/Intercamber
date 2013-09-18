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
    public class ProfilsDao
    {

		public IList<Profils> SearchProfilss()
		{
			return IBatisHelper.Instance().QueryForList<Profils>("CML.Intercamber.Profils.SearchProfilss", null);
		}

		public void UpdateProfils(Profils obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Update("CML.Intercamber.Profils.UpdateProfils", obj);
                session.Complete();
            }
		}

		public void InsertProfils(Profils obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.Profils.InsertProfils", obj);
                session.Complete();
            }
		}
		
		public void DeleteProfils(string code)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
				// TODO  
                //IBatisHelper.Instance().Delete("CML.Intercamber.Profils.DeteleProfils", code);
                //session.Complete();
            }
		}
	}
}

