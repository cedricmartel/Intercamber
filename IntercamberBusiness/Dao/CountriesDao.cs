using System.Collections.Generic;
using System.Linq;

namespace CML.Intercamber.Business.Dao
{
    public class CountriesDao
    {
        public static List<Countries> ListCountries()
        {
            List<Countries> res;
            using (var context = new IntercamberEntities())
            {
                res = context.Countries.ToList();
            }
            return res;
        }
    }
}
