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
    public class ContactsDao
    {
        public IList<ContactDetail> ListContactsByUser(long idUser)
		{
            return IBatisHelper.Instance().QueryForList<ContactDetail>("CML.Intercamber.Contacts.ListContactsByUser", idUser);
		}




		public void UpdateContacts(Contacts obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Update("CML.Intercamber.Contacts.UpdateContacts", obj);
                session.Complete();
            }
		}

		public void InsertContacts(Contacts obj)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
                IBatisHelper.Instance().Insert("CML.Intercamber.Contacts.InsertContacts", obj);
                session.Complete();
            }
		}
		
		public void DeleteContacts(string code)
		{
			using (IDalSession session = IBatisHelper.Instance().BeginTransaction() )
            {
				// TODO  
                //IBatisHelper.Instance().Delete("CML.Intercamber.Contacts.DeteleContacts", code);
                //session.Complete();
            }
		}
	}
}

