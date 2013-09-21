using System;

namespace CML.Intercamber.Business.Helper
{
    /// <summary>
    /// Helper autour de la classe DateTime
    /// </summary>
    public static class DateTimeHelper
    {
        public const string DATETIME_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// Parse une date au format YYYYMMDDHHMISS et retourne un datetime
        /// si la conversion est impossible, retourne null sans lancer d'exception
        /// on peut utiliser comme format DateTimeHelper.DATETIME_FORMAT
        /// </summary>
        public static DateTime? TryParseDate(string dateStr, string dateFormat)
        {
            DateTime? res = null;
            if (!string.IsNullOrEmpty(dateStr))
            {
                try
                {
                    res = DateTime.ParseExact(dateStr, dateFormat, null);
                }
                catch
                {
                    res = null;
                }
            }
            return res;
        }

        /// <summary>
        /// Pour formater une date dans un format donné, on peut utiliser DateTimeHelper.DATETIME_FORMAT
        /// </summary>
        public static string FormatDate(DateTime dateToFormat, string dateFormat)
        {
            string format = "{0:" + dateFormat + "}";
            return String.Format(format, dateToFormat);
        }


        /// <summary>
        /// retourne la date de début de la semaine, commencant un startOfWeek (généralement DayOfWeek.Monday)
        /// </summary>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

    }
}