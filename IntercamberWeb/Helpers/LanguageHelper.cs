using System.Collections.Generic;
using System.Threading;
using CML.Intercamber.Business;
using CML.Intercamber.Business.Dao;

namespace CML.Intercamber.Web.Helpers
{
    public class LanguageHelper
    {
        public static List<Languages> AllLanguages
        {
            get
            {
                List<Languages> data = SessionHelper.GetSessionItem(SessionHelper.SessionKeyLanguages) as List<Languages>;
                if (data == null)
                {
                    data = LanguagesDao.ListLanguages();
                    SessionHelper.AddSessionItem(SessionHelper.SessionKeyLanguages, data);
                }
                return data;
            }
        }

        public static string UiLanguage
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.ToString().Substring(0, 2);
            }
        }

    }
}