using System.Collections.Generic;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class ContactsHelper
    {


        // contacts
        public static IList<ContactDetail> ContactDetails(long idUser, string emailUser)
        {
            IList<ContactDetail> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyContacts, emailUser) as List<ContactDetail>;
            if (data == null)
            {
                var dao = new ContactsDao();
                data = dao.ListContactsByUser(idUser);
                SessionHelper.AddSessionItem(SessionHelper.SessionKeyContacts, data, emailUser);
            }
            return data;
        }
        public static IList<ContactDetail> ContactDetails(long idUser)
        {
            IList<ContactDetail> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyContacts) as List<ContactDetail>;
            if (data == null)
            {
                var dao = new ContactsDao();
                data = dao.ListContactsByUser(idUser);
                SessionHelper.AddSessionItem(SessionHelper.SessionKeyContacts, data);
            }
            return data;
        }

    }
}