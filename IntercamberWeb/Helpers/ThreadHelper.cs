using System.Collections.Generic;
using CML.Intercamber.Business.Dao;
using CML.Intercamber.Business.Model;

namespace CML.Intercamber.Web.Helpers
{
    public class ThreadHelper
    {

        // data threads 
        public static IList<ThreadDetail> ThreadDetails
        {
            get
            {
                IList<ThreadDetail> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyThreads) as List<ThreadDetail>;
                if (data == null)
                    data = ReloadThreadDetails();
                return data;
            }
        }

        // data threads 
        public static List<ThreadDetail> ReloadThreadDetails()
        {
            var dao = new ThreadsDao();
            List<ThreadDetail> data = dao.ListThreads(ConnectedUserHelper.ConnectedUserId);
            SessionHelper.AddSessionItem(SessionHelper.SessionKeyThreads, data);
            return data;
        }

    }
}