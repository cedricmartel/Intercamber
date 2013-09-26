using System.Linq;

namespace CML.Intercamber.Business.Dao
{
    public class UsersDao
    {
        public Users Login(string email, string password)
        {
            Users res;
            using (var context = new IntercamberEntities())
            {
                res = context.Users.FirstOrDefault(x => x.Enabled && x.Password == password && x.Email == email);
            }
            return res;
        }

        public Users ListUserByEmail(string email)
        {
            Users res;
            using (var context = new IntercamberEntities())
            {
                res = context.Users.FirstOrDefault(x => x.Enabled && x.Email == email);
            }
            return res;
        }


        
    }
}

