using System.Collections.Generic;
using System.Linq;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Business.Dao
{
    public class ContactsDao
    {
        public List<ContactDetail> ListContactsByUser(long idUser)
        {
            List<ContactDetail> res;
            using (var context = new IntercamberEntities())
            {
                res = (from c in context.Contacts
                       where c.IdUser == idUser
                       join u in context.Users on c.IdUserContact equals u.IdUser
                       where u.Enabled
                       select new ContactDetail
                       {
                           IdUser = u.IdUser,
                           FirstName = u.FirstName,
                           LastName = u.LastName,

                       }).ToList();
            }
            return res;
        }


    }
}

