using System;
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

        public string Name
        {
            get { return FirstName + " " + LastName; }
        }

        public int? Age
        {
            get
            {
                if (BirthDate == null)
                    return null;
                return DateTimeHelper.Age(BirthDate.Value);
            }
        }

        public bool Connected;

        public string PresentationText
        {
            get
            {
                return "Presentation text writen by " + Name + "<br>His photo will also be visible here";
            }
        }
    }
}
