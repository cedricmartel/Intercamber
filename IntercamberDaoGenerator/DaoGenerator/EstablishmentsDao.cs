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
    public class EstablishmentsDao
    {

		public IList<Establishments> SearchEstablishmentss()
		{
			return IBatisHelper.Instance().QueryForList<Establishments>("CML.Intercamber.Establishments.SearchEstablishmentss", null);
		}

		public void UpdateEstablishments(Establishments obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Update("CML.Intercamber.Establishments.UpdateEstablishments", obj);
                session.Complete();
            }
		}

		public void InsertEstablishments(Establishments obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.Establishments.InsertEstablishments", obj);
                session.Complete();
            }
		}
		
		public void DeleteEstablishments(string code)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
				// TODO  
                //IBatisHelper.Instance().Delete("CML.Intercamber.Establishments.DeteleEstablishments", code);
                //session.Complete();
            }
		}
	}
}

