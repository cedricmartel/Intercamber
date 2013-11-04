using System;
using System.Windows.Markup;
using CML.Intercamber.Business.Helper;

namespace CML.Intercamber.Business.Model
{
    /// <summary>
    /// exchange class for contact search
    /// </summary>
    public class UsersDetail
    {
        public long IdUser { get; set; }
        public int? IdGender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdCountry { get; set; }
        public string City { get; set; }
        public string SpokenLanguages { get; set; }
        public string RequestMessage { get; set; }
        public long NumUnreadMessages { get; set; }

        private string presentationText;

        public string PresentationText
        {
            get
            {
                if (!string.IsNullOrEmpty(presentationText))
                    return presentationText;
                return null;
            }
            set
            {
                presentationText = value;
            }
        }


        public string Name
        {
            get { return FirstName + " " + LastName; }
        }

        public string Age
        {
            get
            {
                if (BirthDate == null)
                    return "?";
                return DateTimeHelper.Age(BirthDate.Value).ToString();
            }
        }

        public bool Connected { get; set; }

        public string Location { get; set;  }

    }
}
