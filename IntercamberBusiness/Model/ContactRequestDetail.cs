using System.Collections.Generic;

namespace CML.Intercamber.Business.Model
{
    public class ContactRequestDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> SpokenLanguages { get; set; }

        public string NameFormated
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
