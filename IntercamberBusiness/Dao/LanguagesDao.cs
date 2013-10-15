using System.Collections.Generic;
using System.Linq;

namespace CML.Intercamber.Business.Dao
{
    public class LanguagesDao
    {
        public static List<Languages> ListLanguages()
        {
            List<Languages> res;
            using (var context = new IntercamberEntities())
            {
                res = context.Languages.ToList();
            }
            return res;
        }
    }
}
