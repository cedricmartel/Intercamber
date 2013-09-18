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
    public class CountryDao
    {

		public IList<Country> SearchCountrys()
		{
			return IBatisHelper.Instance().QueryForList<Country>("CML.Intercamber.Country.SearchCountrys", null);
		}

		public void UpdateCountry(Country obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Update("CML.Intercamber.Country.UpdateCountry", obj);
                session.Complete();
            }
		}

		public void InsertCountry(Country obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.Country.InsertCountry", obj);
                session.Complete();
            }
		}
		
		public void DeleteCountry(string code)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
				// TODO  
                //IBatisHelper.Instance().Delete("CML.Intercamber.Country.DeteleCountry", code);
                //session.Complete();
            }
		}
	}
}

