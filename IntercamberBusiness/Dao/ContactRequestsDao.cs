using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Business.Dao
{
    public class ContactRequestsDao
    {

        /// <summary>
        /// return true if new contact request has been created
        /// </summary>
        public bool InsertContactRequest(ContactRequests v)
        {
            v.DateAsk = DateTime.Now;
            using (var context = new IntercamberEntities())
            {
                // validate that no other request exists 
                var previous = context.ContactRequests.FirstOrDefault(x => x.IdUserAsk == v.IdUserAsk && x.IdUserRequester == v.IdUserRequester && x.DateDismissed == null);
                if (previous != null)
                    return false;
                context.ContactRequests.Add(v);
                context.SaveChanges();
            }
            return true;
        }

        //public List<long> ListIdContactsRequested(long idUserConnected)
        //{
        //    List<long> res;
        //    using (var context = new IntercamberEntities())
        //    {
        //        res = context.ContactRequests.Where(x => x.IdUserRequester == idUserConnected).Select(x => x.IdUserAsk).ToList();
        //    }
        //    return res;
        //}

        /// <summary>
        /// All contacts requests that target user idUserConnected, or made by idUserConnected, and not responded yet
        /// </summary>
        public List<ContactRequestDetail> ListContactRequests(long idUserConnected)
        {
            List<ContactRequestDetail> res;
            using (var context = new IntercamberEntities())
            {
                res = (from x in context.ContactRequests
                    join u in context.Users on x.IdUserRequester equals u.IdUser
                    where x.IdUserAsk == idUserConnected && x.DateDismissed == null && x.DateValidated == null
                    select new
                       {
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           SpokenLanguages = u.UsersSpokenLanguages.Select(y => y.IdLanguage)
                       }).ToList().Select(x => new ContactRequestDetail
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName, 
                        SpokenLanguages = string.Join(",", x.SpokenLanguages)
                    }).ToList();
            }
            return res;
        }

        /// <summary>
        /// if isValidated == true then update contactRequest with validated, and insert into contacts, else update contactRequest
        /// </summary>
        /// <param name="idContactRequest"></param>
        /// <param name="isValidated"></param>
        public void ValidateRefuseContactRequest(long idContactRequest, bool isValidated)
        {
            using (var context = new IntercamberEntities())
            {
                var r = context.ContactRequests.FirstOrDefault(x => x.IdContactRequest == idContactRequest);
                if (r == null)
                    return;
                if (isValidated)
                {
                    r.DateValidated = DateTime.Now;
                    Contacts c1 = new Contacts {IdUser = r.IdUserRequester, IdUserContact = r.IdUserAsk, DateAdd = DateTime.Now};
                    Contacts c2 = new Contacts { IdUser = r.IdUserAsk, IdUserContact = r.IdUserRequester, DateAdd = DateTime.Now };
                    context.Contacts.Add(c1);
                    context.Contacts.Add(c2);
                }
                else
                {
                    r.DateDismissed = DateTime.Now;
                }
                context.SaveChanges();
            }
        }
    }
}
