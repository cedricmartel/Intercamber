using System.Collections.Generic;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Business.Dao
{
    public class UsersDao
    {

        public Users Login(string email, string password)
        {
            Dictionary<string, string> param = new Dictionary<string, string> {{"email", email}, {"password", password}};
            return IBatisHelper.Instance().QueryForObject<Users>("CML.Intercamber.Users.Login", param);
        }

        //public IList<Users> SearchUserss()
        //{
        //    return IBatisHelper.Instance().QueryForList<Users>("CML.Intercamber.Users.SearchUserss", null);
        //}

        //public void UpdateUsers(Users obj)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction())
        //    {
        //        IBatisHelper.Instance().Update("CML.Intercamber.Users.UpdateUsers", obj);
        //        session.Complete();
        //    }
        //}

        //public void InsertUsers(Users obj)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction())
        //    {
        //        IBatisHelper.Instance().Insert("CML.Intercamber.Users.InsertUsers", obj);
        //        session.Complete();
        //    }
        //}

        //public void DeleteUsers(string code)
        //{
        //    using (IDalSession session = IBatisHelper.Instance().BeginTransaction())
        //    {
        //        // TODO  
        //        //IBatisHelper.Instance().Delete("CML.Intercamber.Users.DeteleUsers", code);
        //        //session.Complete();
        //    }
        //}
    }
}

