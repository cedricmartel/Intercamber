using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CML.Intercamber.Business.Model;
using MvcJqGrid;

namespace CML.Intercamber.Business.Dao
{
    public class UsersDao
    {
        public Users Login(string email, string password)
        {
            Users res;
            using (var context = new IntercamberEntities())
            {
                res = context.Users.Where(x => x.Enabled && x.Password == password && x.Email == email).Include("UsersLearnLanguages").Include("UsersSpokenLanguages").FirstOrDefault();
            }
            return res;
        }

        public Users FindUserByEmail(string email)
        {
            Users res;
            using (var context = new IntercamberEntities())
            {
                res = context.Users.Where(x => x.Enabled && x.Email == email).Include("UsersLearnLanguages").Include("UsersSpokenLanguages").FirstOrDefault();
            }
            return res;
        }

        public GridData<UsersDetail> SearchUsers(GridSettings gridSettings, long userId, List<string> spokenLanguages, List<string> learnLanguages)
        {
            List<UsersDetail> res;
            int totalRecords = 0;
            int totalPages = 0;
            using (var context = new IntercamberEntities())
            {
                var req = (from u in context.Users
                           where u.Enabled //&& u.IdProfil == ProfilsConst.Student.Id
                                && learnLanguages.Any(x => u.UsersSpokenLanguages.Any(y => y.IdLanguage == x))
                                && spokenLanguages.Any(x => u.UsersLearnLanguages.Any(y => y.IdLanguage == x))
                                && !context.Contacts.Any(x => x.IdUser == userId && x.IdUserContact == u.IdUser)
                           select new UsersDetail
                           {
                               IdUser = u.IdUser,
                               LastName = u.LastName,
                               FirstName = u.FirstName,
                               BirthDate = u.BirthDate,
                               IdCountry = u.IdCountry,
                               IdGender = u.IdGender,
                               City = u.City
                           })
                       .OrderBy(x => x.IdUser);
                totalRecords = req.Count();
                if(gridSettings.PageSize > 0)
                    totalPages = (int)Math.Ceiling((decimal)totalRecords/(decimal)gridSettings.PageSize);
                res = req.Skip(gridSettings.PageSize * (gridSettings.PageIndex - 1))
                       .Take(gridSettings.PageSize)
                       .ToList();
            }

            return new GridData<UsersDetail>
            {
                page = gridSettings.PageIndex, // number of requested page
                records = totalRecords, // total records from query 
                total = totalPages, // total pages of query 
                rows = res
            };
        }


    }
}

