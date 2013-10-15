using System.Collections.Generic;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;

namespace CML.Intercamber.Web.Helpers
{
    public class CountryHelper
    {
        public static List<Countries> AllCountries
        {
            get
            {
                List<Countries> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyCountries) as List<Countries>;
                if (data == null)
                {
                    data = CountriesDao.ListCountries();
                    SessionHelper.AddSessionItem(SessionHelper.SessionKeyCountries, data);
                }
                return data;
            }
        } 
    }
}