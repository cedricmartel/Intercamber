using System;
using System.Collections.Generic;
using System.Linq;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Business.Dao
{
    public class ContactsDao
    {
        public List<UsersDetail> ListContactsByUser(long idUser)
        {
            List<UsersDetail> res;
            using (var context = new IntercamberEntities())
            {
                res = (from c in context.Contacts
                    where c.IdUser == idUser
                    join u in context.Users on c.IdUserContact equals u.IdUser
                    where u.Enabled && u.DisplayInContactRequests
                    select new
                    {
                        u.IdUser,
                        u.FirstName,

                        u.LastName,
                        u.BirthDate,
                        u.IdCountry,
                        u.IdGender,
                        u.City,
                        SpokenLanguages = u.UsersSpokenLanguages.Select(y => y.IdLanguage)
                    }).ToList().Select(x => new UsersDetail
                    {
                        IdUser = x.IdUser,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        IdCountry = x.IdCountry,
                        IdGender = x.IdGender,
                        City = x.City,
                        SpokenLanguages = string.Join(",", x.SpokenLanguages),
                    }).ToList();
            }
            return res;
        }
    
    }
}

