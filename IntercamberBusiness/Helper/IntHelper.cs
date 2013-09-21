
namespace CML.Intercamber.Business.Helper
{
    public class IntHelper
    {
        /// <summary>
        /// fait un int.tryparse avec prise en charge d'une valeur par defaut
        /// </summary>
        public static int TryParseDefault(string stringToParse, int defaultValue)
        {
            int res = defaultValue;
            if (!string.IsNullOrEmpty(stringToParse))
            {
                int.TryParse(stringToParse, out res);
            }
            return res;
        }
    }
}
